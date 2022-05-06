using Moq;
using Prism.Regions;
using System;
using WpfSample.Core;
using WpfSample.Data.Model;
using WpfSample.Modules.Activities.ViewModels;
using WpfSample.Modules.Activities.Views;
using WpfSample.Services.Abstractions;
using Xunit;

namespace WpfSample.Modules.Tests.Activities.ViewModels
{
    public class ActivitiesListViewModelTests
    {
        private readonly Activity[] activitiesList = new Activity[] {
            new Activity { Id = 10 },
            new Activity { Id = 20 },
            new Activity { Id = 30 }
        };
        private readonly Mock<IDataService<Activity>> _dataServiceMock;
        private readonly Mock<IRegionManager> _regionManagerMock;
        private readonly Mock<IRegionNavigationService> _regionNavigationServiceMock;
        private readonly Mock<IRegion> _regionMock;

        public ActivitiesListViewModelTests()
        {
            _dataServiceMock = new Mock<IDataService<Activity>>();
            _dataServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(activitiesList);
            _dataServiceMock.Setup(x => x.Create(It.IsAny<Activity>()))
                .Returns<Activity>(t => { 
                    t.CreatedOn = DateTime.UtcNow; 
                    return t; 
                });

            _regionManagerMock = new Mock<IRegionManager>();
            _regionMock = new Mock<IRegion>();

            _regionNavigationServiceMock = new Mock<IRegionNavigationService>();
            _regionNavigationServiceMock.Setup(x => x.Region).Returns(_regionMock.Object);
        }

        [Fact]
        public void ActivitiesListViewModel_Initialization()
        {
            var vm = new ActivitiesListViewModel(_regionManagerMock.Object, _dataServiceMock.Object);

            _dataServiceMock.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(vm.View);
            Assert.False(vm.View.IsEmpty);
        }

        [Fact]
        public void ActivitiesListViewModel_Initialization_NullParameters()
        {
            Assert.Throws<ArgumentNullException>(() => new ActivitiesListViewModel(null, null));
        }

        [Fact]
        public void ActivitiesListViewModel_SearchText_INotifyPropertyChangedCalled()
        {
            var vm = new ActivitiesListViewModel(_regionManagerMock.Object, _dataServiceMock.Object);
            Assert.PropertyChanged(vm, nameof(vm.SearchText), () => vm.SearchText = "filter");
            Assert.Equal("filter", vm.SearchText);
        }

        [Fact]
        public void ActivitiesListViewModel_ClearFilter()
        {
            var vm = new ActivitiesListViewModel(_regionManagerMock.Object, _dataServiceMock.Object);

            vm.SearchText = null;

            Assert.Null(vm.View.Filter);
            Assert.False(vm.View.IsEmpty);
        }

        [Fact]
        public void ActivitiesListViewModel_Filter()
        {
            var vm = new ActivitiesListViewModel(_regionManagerMock.Object, _dataServiceMock.Object);

            vm.SearchText = "filter";

            Assert.NotNull(vm.View.Filter);
            Assert.True(vm.View.IsEmpty);

            Assert.False(vm.View.Filter(new object()));
            Assert.True(vm.View.Filter(new ActivityViewModel { Name = "str with filter" }));
        }

        [Fact]
        public void ActivitiesListViewModel_AddItemCommand_Execute()
        {
            var vm = new ActivitiesListViewModel(_regionManagerMock.Object, _dataServiceMock.Object);

            Assert.NotNull(vm.AddItemCommand);
            Assert.True(vm.AddItemCommand.CanExecute(null));

            vm.AddItemCommand.Execute(null);

            _regionManagerMock.Verify(x => x.RequestNavigate(
                    It.Is<string>(s => s == RegionNames.ContentRegion),
                    It.Is<string>(s => s == nameof(CreateActivityView))
                ), Times.Once);
        }

        [Fact]
        public void ActivitiesListViewModel_OnNavigatedTo_WithEmptyParameter()
        {
            var vm = new ActivitiesListViewModel(_regionManagerMock.Object, _dataServiceMock.Object);

            vm.OnNavigatedTo(new NavigationContext(_regionNavigationServiceMock.Object, new Uri("http://test")));

            _dataServiceMock.Verify(x => x.Create(It.IsAny<Activity>()), Times.Never);
        }

        [Fact]
        public void ActivitiesListViewModel_OnNavigatedTo_WithNewItem()
        {
            var vm = new ActivitiesListViewModel(_regionManagerMock.Object, _dataServiceMock.Object);

            var newActivity = new ActivityViewModel { Id = 50, Name = "activity name" };
            var param = new NavigationParameters
            {
                { Constants.NewItemParamName, newActivity }
            };
            var navigationContext = new NavigationContext(_regionNavigationServiceMock.Object, new Uri("http://test"), param);

            vm.OnNavigatedTo(navigationContext);

            _dataServiceMock.Verify(x => x.Create(
                It.Is<Activity>(s => s.Id == newActivity.Id && s.Name == newActivity.Name)),
                Times.Once);

            Assert.Equal(newActivity.Id, vm.SelectedActivity.Id);
            Assert.Equal(newActivity.Name, vm.SelectedActivity.Name);
        }
    }
}
