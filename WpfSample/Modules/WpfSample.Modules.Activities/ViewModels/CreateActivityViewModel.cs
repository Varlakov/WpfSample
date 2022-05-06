using Prism.Commands;
using Prism.Regions;
using System.Windows.Input;
using WpfSample.Core;
using WpfSample.Core.ViewModels;
using WpfSample.Modules.Activities.Views;

namespace WpfSample.Modules.Activities.ViewModels
{
    public class CreateActivityViewModel : RegionViewModelBase
    {
        private ActivityViewModel _activityViewModel;
        public ActivityViewModel Activity
        {
            get => _activityViewModel;
            set => SetProperty(ref _activityViewModel, value);
        }


        private DelegateCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= new DelegateCommand(SaveCommandExecute, SaveCanExecute);


        private DelegateCommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand ??= new DelegateCommand(CancelCommandExecute);


        public CreateActivityViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            Activity = new ActivityViewModel();
            Activity.PropertyChanged += ActivityViewModelPropertyChanged;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
            Activity.PropertyChanged -= ActivityViewModelPropertyChanged;
        }

        private void ActivityViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _saveCommand.RaiseCanExecuteChanged();
        }

        private void SaveCommandExecute()
        {
            var param = new NavigationParameters
            {
                { Constants.NewItemParamName, Activity }
            };

            RegionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ActivitiesListView), param);
        }

        private bool SaveCanExecute() => Activity != null && Activity.Error == null;

        private void CancelCommandExecute()
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ActivitiesListView));
        }
    }
}
