using Microsoft.EntityFrameworkCore;
using System;
using WpfSample.Data;

namespace WpfSample.Services.Tests
{
    public class InMemoryDbContextFixture : IDisposable
    {
        public static readonly int ItemsNumber = 200;

        public InMemoryDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<SampleDataDbContext>()
                    .UseInMemoryDatabase($"InMemoryTestDb{Guid.NewGuid()}")
                    .Options;

            using var context = new SampleDataDbContext(options);
            context.Equipments.AddRange(TestDataGenerator.GetTestEquipments(ItemsNumber));
            context.Activities.AddRange(TestDataGenerator.GetTestActivities(ItemsNumber));
            context.SaveChanges();

            Options = options;
        }

        public void Dispose()
        {
            using var context = new SampleDataDbContext(Options);
            context.Database.EnsureDeleted();
        }

        public DbContextOptions<SampleDataDbContext> Options { get; private set; }
    }
}
