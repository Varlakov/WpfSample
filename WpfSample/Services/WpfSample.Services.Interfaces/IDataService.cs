using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfSample.Data.Model;

namespace WpfSample.Services.Abstractions
{
    public interface IDataService<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAllAsync();

        T Create(T entity);
    }
}
