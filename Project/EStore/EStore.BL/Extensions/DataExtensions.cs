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

        public static List<ProductCategoryItem> AllCategories(this EStoreEntities context)
        {
            var categoryItems = context.Set<tblProductCategory>()
                .Select(x => new ProductCategoryItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentCategoryId = x.ParentCategoryId,
                    ParentCategoryName = x.ParentCategoryId != null ? x.tblProductCategory2.Name : ""
                })
                .ToList();

            return categoryItems;
        }
    }
}