using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSample.Data.Model;

namespace WpfSample.Services.Abstractions
{
    public interface IPagingDataService<T> where T : EntityBase
    {
        Task<int> TotalCountAsync();

        Task<IEnumerable<T>> GetAllAsync(int from, int count);
    }
}
