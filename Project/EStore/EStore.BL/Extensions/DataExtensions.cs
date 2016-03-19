using System.Collections.Generic;
using System.Linq;
using EStore.BL.Models;
using EStore.DL.Mapping;

namespace EStore.BL.Extensions
{
    public static class DataExtensions
    {
        public static List<SupplierItem> AllSuppliers(this EStoreEntities context)
        {
            var items = context.Set<tblSupplier>()
                .Select(x => new SupplierItem
                {
                    Name = x.Name,
                    Id = x.Id
                })
                .OrderBy(x => x.Name)
                .ToList();

            return items;
        }
    }
}