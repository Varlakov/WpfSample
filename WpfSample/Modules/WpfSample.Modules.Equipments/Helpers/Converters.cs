using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSample.Data.Model;
using WpfSample.Modules.Equipments.ViewModels;

namespace WpfSample.Modules.Equipments.Helpers
{
    public static class Converters
    {
        public static EquipmentViewModel ToViewModel(this Equipment entity) =>
            new EquipmentViewModel
            {
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                Id = entity.Id,
                Name = entity.Name,
                LastModifiedOn = entity.LastModifiedOn,
                Quantity = entity.Quantity,
                Type = entity.Type,
            };
    }
}
