using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using WpfSample.Core;
using WpfSample.Core.ViewModels;
using WpfSample.Data.Model;
using WpfSample.Modules.Activities.Helpers;
using WpfSample.Modules.Activities.Views;
using WpfSample.Services.Abstractions;

namespace WpfSample.Modules.Activities.ViewModels
{
    public class ActivitiesListViewModel : RegionViewModelBase
    {
        private readonly IDataService<Activity> _activitiesDataService;
        private readonly ObservableCollection<ActivityViewModel> _allActivitiesList;


        private ActivityViewModel _selectedActivity;
        public ActivityViewModel SelectedActivity
        {
            get { return _selectedActivity; }
            set { SetProperty(ref _selectedActivity, value); }
        }


        private readonly ListCollectionView _view;
        public ICollectionView View
        {
            get { return _view; }
        }


        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);

                if (string.IsNullOrEmpty(_searchText))
                    View.Filter = null;
                else
                    View.Filter = new Predicate<object>(o =>
                    {

                        if (o is ActivityViewModel vm && vm.Name != null)
                        {
                            return vm.Name.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase);
                        }

                        return false;
                    });
            }
        }


        private DelegateCommand _addItemCommand;
        public ICommand AddItemCommand => _addItemCommand ??= new DelegateCommand(AddItemCommandExecute);


        public ActivitiesListViewModel(IRegionManager regionManager, IDataService<Activity> activitiesDataService) :
            base(regionManager)
        {
            _activitiesDataService = activitiesDataService ?? throw new ArgumentNullException(nameof(activitiesDataService));

            var allActivities = _activitiesDataService.GetAllAsync().GetAwaiter().GetResult().Select(t => t.ToViewModel());

            _allActivitiesList = new ObservableCollection<ActivityViewModel>(allActivities);
            _view = new ListCollectionView(_allActivitiesList);
        }


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.TryGetValue(Constants.NewItemParamName, out ActivityViewModel newActivity))
            {
                var activity = newActivity.ToModel();
                var savedEntity = _activitiesDataService.Create(activity);

                var savedViewModel = savedEntity.ToViewModel();

                _allActivitiesList.Add(savedViewModel);

                SelectedActivity = savedViewModel;
            }
        }

        private void AddItemCommandExecute()
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegion, nameof(CreateActivityView));
        }
    }
}
