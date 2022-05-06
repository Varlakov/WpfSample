using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSample.Data.Model;

namespace WpfSample.Services.Abstractions
{
    public interface IEquipmentDataService : IDataService<Equipment>, IPagingDataService<Equipment>
    {
    }
}
