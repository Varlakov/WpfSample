using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using WpfSample.Data;
using WpfSample.Data.Model;
using WpfSample.Modules.Activities;
using WpfSample.Modules.Equipments;
using WpfSample.Services;
using WpfSample.Services.Abstractions;
using WpfSample.Views;

namespace WpfSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDbContextFactory<DbContext>>(() =>
            {

                var builder = new DbContextOptionsBuilder<SampleDataDbContext>();
                builder.UseInMemoryDatabase("WpfSampleDatabase");
                var options = builder.Options;

                var factory = new SampleDataDbContextFactory(options);

                using (var context = factory.CreateDbContext())
                {
                    context.AddRange(TestDataGenerator.GetTestEquipments());
                    context.AddRange(TestDataGenerator.GetTestActivities());

                    context.SaveChanges();
                }

                return factory;
            });

            containerRegistry.RegisterScoped<IDataService<Activity>, DataService<Activity>>();
            containerRegistry.RegisterScoped<IEquipmentDataService, EquipmentDataService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ActivitiesModule>();
            moduleCatalog.AddModule<EquipmentsModule>();
        }
    }
}
