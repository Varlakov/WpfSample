using Moq;
using Prism.Regions;
using System;
using System.Linq;
using WpfSample.Core;
using WpfSample.Data.Model;
using WpfSample.Modules.Activities.ViewModels;
using WpfSample.Modules.Activities.Views;
using WpfSample.Modules.Equipments.ViewModels;
using WpfSample.Services.Abstractions;
using Xunit;

namespace WpfSample.Modules.Tests.Equipments.ViewModels
{
    public class EquipmentsListViewModelTests
    {
        private readonly Equipment[] equipmentsList = new Equipment[] {
            new Equipment { Id = 10 },
            new Equipment { Id = 20 },
            new Equipment { Id = 30 }
        };
        private readonly Mock<IEquipmentDataService> _dataServiceMock;
        private readonly Mock<IRegionManager> _regionManagerMock;

        public EquipmentsListViewModelTests()
        {
            _dataServiceMock = new Mock<IEquipmentDataService>();
            _dataServiceMock
                .Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(equipmentsList);

            _dataServiceMock
                .Setup(x => x.TotalCountAsync())
                .ReturnsAsync(equipmentsList.Length);

            _regionManagerMock = new Mock<IRegionManager>();
        }

        [Fact]
        public void EquipmentsListViewModel_Initialization()
        {
            var vm = new EquipmentsListViewModel(_regionManagerMock.Object, _dataServiceMock.Object);

            _dataServiceMock.Verify(x => x.TotalCountAsync(), Times.Once);

            Assert.NotNull(vm.Equipments);
            Assert.NotNull(vm.PaginationViewModel);
            Assert.NotNull(vm.SelectedEquipment);

            Assert.True(vm.Equipments.Any());
        }

        [Fact]
        public void EquipmentsListViewModel_Initialization_NullParameters()
        {
            Assert.Throws<ArgumentNullException>(() => new EquipmentsListViewModel(null, null));
        }

        [Fact]
        public void EquipmentsListViewModel_SelectedEquipment_INotifyPropertyChangedCalled()
        {
            var vm = new EquipmentsListViewModel(_regionManagerMock.Object, _dataServiceMock.Object);
            Assert.PropertyChanged(vm, nameof(vm.SelectedEquipment), () => vm.SelectedEquipment = new EquipmentViewModel());
        }

        [Fact]
        public void EquipmentsListViewModel_Destroy()
        {
            var vm = new EquipmentsListViewModel(_regionManagerMock.Object, _dataServiceMock.Object);

            var selectedItem = vm.SelectedEquipment;

            vm.Destroy();

            vm.PaginationViewModel.ShowNextPage();

            Assert.Same(selectedItem, vm.SelectedEquipment);
        }
    }
}
