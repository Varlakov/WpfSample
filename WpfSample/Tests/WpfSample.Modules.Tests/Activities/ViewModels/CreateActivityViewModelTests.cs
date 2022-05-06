using Moq;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSample.Core;
using WpfSample.Modules.Activities.ViewModels;
using WpfSample.Modules.Activities.Views;
using Xunit;

namespace WpfSample.Modules.Tests.Activities.ViewModels
{
    public class CreateActivityViewModelTests
    {
        private readonly Mock<IRegionManager> _regionManagerMock;
        private readonly Mock<IRegionNavigationService> _regionNavigationServiceMock;
        private readonly Mock<IRegion> _regionMock;


        public CreateActivityViewModelTests()
        {
            _regionManagerMock = new Mock<IRegionManager>();
            _regionMock = new Mock<IRegion>();

            _regionNavigationServiceMock = new Mock<IRegionNavigationService>();
            _regionNavigationServiceMock.Setup(x => x.Region).Returns(_regionMock.Object);
        }


        [Fact]
        public void CreateActivityViewModel_Activity_INotifyPropertyChangedCalled()
        {
            var vm = new CreateActivityViewModel(_regionManagerMock.Object);
            Assert.PropertyChanged(vm, nameof(vm.Activity), () => vm.Activity = new ActivityViewModel());
        }

        [Fact]
        public void CreateActivityViewModel_CancelCommand_Execute()
        {
            var vm = new CreateActivityViewModel(_regionManagerMock.Object);

            Assert.NotNull(vm.CancelCommand);
            Assert.True(vm.CancelCommand.CanExecute(null));

            vm.CancelCommand.Execute(null);

            _regionManagerMock.Verify(x => x.RequestNavigate(
                    It.Is<string>(s => s == RegionNames.ContentRegion),
                    It.Is<string>(s => s == nameof(ActivitiesListView))
                ), Times.Once);
        }

        [Fact]
        public void CreateActivityViewModel_OnNavigatedTo()
        {
            var vm = new CreateActivityViewModel(_regionManagerMock.Object);

            vm.OnNavigatedTo(new NavigationContext(_regionNavigationServiceMock.Object, new Uri("http://test")));

            Assert.NotNull(vm.Activity);
        }

        [Fact]
        public void CreateActivityViewModel_SaveCommand_Execute_WithFailedValidation()
        {
            var vm = new CreateActivityViewModel(_regionManagerMock.Object)
            {
                Activity = new ActivityViewModel()
                {
                    Name = string.Empty
                }
            };

            Assert.NotNull(vm.SaveCommand);
            Assert.False(vm.SaveCommand.CanExecute(null));

            vm.SaveCommand.Execute(null);

            _regionManagerMock.Verify(x => x.RequestNavigate(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void CreateActivityViewModel_SaveCommand_Execute_WithEmptyActivity()
        {
            var vm = new CreateActivityViewModel(_regionManagerMock.Object);

            Assert.NotNull(vm.SaveCommand);
            Assert.False(vm.SaveCommand.CanExecute(null));

            vm.SaveCommand.Execute(null);

            _regionManagerMock.Verify(x => x.RequestNavigate(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void CreateActivityViewModel_SaveCommand_Execute()
        {
            var vm = new CreateActivityViewModel(_regionManagerMock.Object)
            {
                Activity = new ActivityViewModel()
                {
                    Name = "new name",
                    CreatedBy = "user name"
                }
            };

            Assert.NotNull(vm.SaveCommand);
            Assert.True(vm.CancelCommand.CanExecute(null));

            vm.SaveCommand.Execute(null);

            _regionManagerMock.Verify(x => x.RequestNavigate(
                    It.Is<string>(s => s == RegionNames.ContentRegion),
                    It.Is<string>(s => s == nameof(ActivitiesListView)),
                    It.Is<NavigationParameters>(s => s[Constants.NewItemParamName] != null && s[Constants.NewItemParamName].Equals(vm.Activity))
                ), Times.Once);
        }

        [Fact]
        public void CreateActivityViewModel_SaveCommand_RaiseCanExecuteChanged()
        {
            var vm = new CreateActivityViewModel(_regionManagerMock.Object);

            vm.OnNavigatedTo(new NavigationContext(_regionNavigationServiceMock.Object, new Uri("http://test")));

            var eventWasRised = false;

            vm.SaveCommand.CanExecuteChanged += (s, e) => eventWasRised = true;

            vm.Activity.CreatedOn = DateTime.Now;

            Assert.True(eventWasRised);
        }

        [Fact]
        public void CreateActivityViewModel_OnNavigatedFrom()
        {
            var vm = new CreateActivityViewModel(_regionManagerMock.Object);
            
            var canExecuteChangedWasInvoked = false;

            vm.SaveCommand.CanExecuteChanged += (s, e) => canExecuteChangedWasInvoked = true;

            vm.OnNavigatedTo(new NavigationContext(_regionNavigationServiceMock.Object, new Uri("http://test")));
            vm.Activity.Id = 200;

            Assert.True(canExecuteChangedWasInvoked);

            canExecuteChangedWasInvoked = false;

            vm.OnNavigatedFrom(new NavigationContext(_regionNavigationServiceMock.Object, new Uri("http://test")));

            vm.Activity.Id = 300;

            Assert.False(canExecuteChangedWasInvoked);
        }
    }
}
