using WpfSample.Core.ViewModels;

namespace WpfSample.Modules.Equipments.ViewModels
{
    public class EquipmentViewModel : EntityViewModelBase
    {
        private string _type;
        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }


        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
    }
}
