using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfSample.Data.Model;
using WpfSample.Services.Abstractions;

namespace WpfSample.Services
{
    public class DataService<T> : IDataService<T> where T : EntityBase
    {
        protected readonly IDbContextFactory<DbContext> ContextFactory;

        public DataService(IDbContextFactory<DbContext> contextFactory)
        {
            ContextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }


        public T Create(T entity)
        {
            using DbContext context = ContextFactory.CreateDbContext();

            entity.CreatedOn = DateTime.UtcNow;
            EntityEntry<T> createdResult = context.Set<T>().Add(entity);
            context.SaveChanges();

            return createdResult.Entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using DbContext context = ContextFactory.CreateDbContext();
            return await context.Set<T>().ToListAsync();
        }
    }
}
