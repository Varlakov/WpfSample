using Prism.Ioc;
using Prism.Modularity;
using WpfSample.Modules.Activities.Views;

namespace WpfSample.Modules.Activities
{
    public class ActivitiesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Interface implementation method
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ActivitiesListView>();
            containerRegistry.RegisterForNavigation<CreateActivityView>();
        }
    }
}