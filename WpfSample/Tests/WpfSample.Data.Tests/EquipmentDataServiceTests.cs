using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using WpfSample.Data;
using Xunit;

namespace WpfSample.Services.Tests
{
    public class EquipmentDataServiceTests : IClassFixture<InMemoryDbContextFixture>
    {
        private readonly InMemoryDbContextFixture _dbFixture;
        private readonly Mock<IDbContextFactory<DbContext>> _mockDbFactory;

        public EquipmentDataServiceTests(InMemoryDbContextFixture dbFixture)
        {
            _dbFixture = dbFixture;

            _mockDbFactory = new();
            _mockDbFactory.Setup(f => f.CreateDbContext()).Returns(new SampleDataDbContext(_dbFixture.Options));
        }


        [Fact]
        public async Task EquipmentDataService_TotalCountAsync()
        {
            var ds = new EquipmentDataService(_mockDbFactory.Object);

            var count = await ds.TotalCountAsync();

            Assert.Equal(InMemoryDbContextFixture.ItemsNumber, count);
        }

        [Fact]
        public async Task EquipmentDataService_GetAllAsync()
        {
            var ds = new EquipmentDataService(_mockDbFactory.Object);

            var items = await ds.GetAllAsync(0, InMemoryDbContextFixture.ItemsNumber);

            Assert.Equal(InMemoryDbContextFixture.ItemsNumber, items.Count());
        }

        [Fact]
        public async Task EquipmentDataService_GetAllAsync_Getpage()
        {
            var ds = new EquipmentDataService(_mockDbFactory.Object);

            var items = await ds.GetAllAsync(10, 50);

            Assert.Equal(50, items.Count());
        }
    }
}