using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfSample.Core;
using WpfSample.Core.ViewModels;
using WpfSample.Modules.Equipments.Helpers;
using WpfSample.Services.Abstractions;

namespace WpfSample.Modules.Equipments.ViewModels
{
    public class EquipmentsListViewModel : RegionViewModelBase
    {
        private readonly IEquipmentDataService _equipmentsDataService;

        public ObservableCollection<EquipmentViewModel> Equipments
        {
            get;
            private set;
        }

        public PaginationViewModel PaginationViewModel
        {
            get;
            private set;
        }


        private EquipmentViewModel _selectedEquipment;
        public EquipmentViewModel SelectedEquipment
        {
            get { return _selectedEquipment; }
            set { SetProperty(ref _selectedEquipment, value); }
        }


        public EquipmentsListViewModel(IRegionManager regionManager, IEquipmentDataService equipmentsDataService) : base(regionManager)
        {
            _equipmentsDataService = equipmentsDataService ?? throw new ArgumentNullException(nameof(equipmentsDataService));

            Equipments = new ObservableCollection<EquipmentViewModel>();

            PaginationViewModel = new PaginationViewModel(_equipmentsDataService.TotalCountAsync().GetAwaiter().GetResult());
            PaginationViewModel.OnPageChanged += PaginationViewModelOnPageChanged;

            PaginationViewModelOnPageChanged(null, 0);
        }


        private void PaginationViewModelOnPageChanged(object sender, int e)
        {
            var filtered = _equipmentsDataService.GetAllAsync(e * Constants.TotalItemsPerPage, Constants.TotalItemsPerPage).GetAwaiter().GetResult();
            var t = filtered.Select(t => t.ToViewModel());

            SelectedEquipment = t.ElementAt(0);

            Equipments.Clear();
            Equipments.AddRange(t);
        }

        public override void Destroy()
        {
            base.Destroy();

            PaginationViewModel.OnPageChanged -= PaginationViewModelOnPageChanged;
        }
    }
}
