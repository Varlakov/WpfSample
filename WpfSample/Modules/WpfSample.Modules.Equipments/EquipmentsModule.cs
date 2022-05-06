using Prism.Ioc;
using Prism.Modularity;
using WpfSample.Modules.Equipments.Views;

namespace WpfSample.Modules.Equipments
{
    public class EquipmentsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Interface implementation method
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<EquipmentsListView>();
        }
    }
}