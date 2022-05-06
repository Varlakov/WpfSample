using WpfSample.Modules.Equipments.ViewModels;
using Xunit;

namespace WpfSample.Modules.Tests.Equipments.ViewModels
{
    public class EquipmentViewModelTests
    {
        [Fact]
        public void EquipmentViewModel_Quantity_INotifyPropertyChangedCalled()
        {
            var vm = new EquipmentViewModel();
            Assert.PropertyChanged(vm, nameof(vm.Quantity), () => vm.Quantity = 10);
            Assert.Equal(10, vm.Quantity);
        }

        [Fact]
        public void EquipmentViewModel_Type_INotifyPropertyChangedCalled()
        {
            var vm = new EquipmentViewModel();
            Assert.PropertyChanged(vm, nameof(vm.Type), () => vm.Type = "new type");
            Assert.Equal("new type", vm.Type);
        }
    }
}