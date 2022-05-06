using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WpfSample.Core;

namespace WpfSample.ViewModels
{
    public class NavigationViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public ICommand NavigateToActivitiesCommand { get; private set; }

        public ICommand NavigateToEquipmentsCommand { get; private set; }


        public NavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            
            NavigateToActivitiesCommand = new DelegateCommand(NavigateToActivities);
            NavigateToEquipmentsCommand = new DelegateCommand(NavigateToEquipments);
        }

        private void NavigateToEquipments()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(Modules.Equipments.Views.EquipmentsListView));
        }

        private void NavigateToActivities()
        { 
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(Modules.Activities.Views.ActivitiesListView));
        }
    }
}
