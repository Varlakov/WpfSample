using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using WpfSample.Data;
using WpfSample.Data.Model;
using Xunit;

namespace WpfSample.Services.Tests
{
    public class DataServiceTests
    {
        private static readonly int itemsNumber = 200;

        public DataServiceTests()
        {

        }

        [Fact]
        public void DataService_FailedInitialization()
        {
            Assert.Throws<ArgumentNullException>(() => new DataService<Activity>(null));
        }

        [Fact]
        public async Task DataService_GetAllAsync()
        {
            var options = new DbContextOptionsBuilder<SampleDataDbContext>()
                    .UseInMemoryDatabase("InMemoryTest_DataService_GetAllAsync")
                    .Options;

            using var context = new SampleDataDbContext(options);
            context.Activities.AddRange(TestDataGenerator.GetTestActivities(itemsNumber));
            context.SaveChanges();

            Mock<IDbContextFactory<DbContext>> mockDbFactory = new();
            mockDbFactory.Setup(f => f.CreateDbContext()).Returns(context);

            var ds = new DataService<Activity>(mockDbFactory.Object);

            var items = await ds.GetAllAsync();

            Assert.Equal(itemsNumber, items.Count());
        }

        [Fact]
        public async Task DataService_GetAllAsync_Empty()
        {
            var options = new DbContextOptionsBuilder<SampleDataDbContext>()
                    .UseInMemoryDatabase("InMemoryTest_DataService_GetAllAsync_Empty")
                    .Options;

            using var context = new SampleDataDbContext(options);

            Mock<IDbContextFactory<DbContext>> mockDbFactory = new();
            mockDbFactory.Setup(f => f.CreateDbContext()).Returns(context);

            var ds = new DataService<Activity>(mockDbFactory.Object);

            var items = await ds.GetAllAsync();

            Assert.Empty(items);
        }

        [Fact]
        public void DataService_Create()
        {
            var options = new DbContextOptionsBuilder<SampleDataDbContext>()
                    .UseInMemoryDatabase("InMemoryTest_Create")
                    .Options;

            using var context = new SampleDataDbContext(options);

            Mock<IDbContextFactory<DbContext>> mockDbFactory = new();
            mockDbFactory.Setup(f => f.CreateDbContext()).Returns(context);

            var ds = new DataService<Activity>(mockDbFactory.Object);

            var itemToAdd = new Activity { Name = "name", CreatedBy = "user" };

            var item = ds.Create(itemToAdd);

            Assert.Equal(itemToAdd.Name, item.Name);
            Assert.NotEqual(default, item.CreatedOn);
            Assert.NotEqual(default, item.Id);
        }
    }
}