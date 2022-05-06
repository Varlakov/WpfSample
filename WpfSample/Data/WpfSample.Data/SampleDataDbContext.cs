using Microsoft.EntityFrameworkCore;
using WpfSample.Data.EntityConfigurations;
using WpfSample.Data.Model;

namespace WpfSample.Data
{
    public class SampleDataDbContext : DbContext
    {
        public DbSet<Activity> Activities => Set<Activity>();

        public DbSet<Equipment> Equipments => Set<Equipment>();


        public SampleDataDbContext(DbContextOptions<SampleDataDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ActivityConfiguration());
            modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
        }
    }
}