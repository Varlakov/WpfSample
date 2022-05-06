using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfSample.Data.Model;
using WpfSample.Services.Abstractions;

namespace WpfSample.Services
{
    public class EquipmentDataService : DataService<Equipment>, IEquipmentDataService
    {
        public EquipmentDataService(IDbContextFactory<DbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<IEnumerable<Equipment>> GetAllAsync(int from, int count)
        {
            using DbContext context = ContextFactory.CreateDbContext();
            return await context.Set<Equipment>().Skip(from).Take(count).ToListAsync();
        }

        public async Task<int> TotalCountAsync()
        {
            using DbContext context = ContextFactory.CreateDbContext();
            return await context.Set<Equipment>().CountAsync();
        }
    }
}
