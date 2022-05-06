using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSample.Data
{
    public class SampleDataDbContextFactory : IDbContextFactory<DbContext>
    {
        private readonly DbContextOptions<SampleDataDbContext> _options;

        public SampleDataDbContextFactory(DbContextOptions<SampleDataDbContext> options)
        {
            _options = options;
        }

        public virtual DbContext CreateDbContext()
        {
            return new SampleDataDbContext(_options);
        }
    }
}
