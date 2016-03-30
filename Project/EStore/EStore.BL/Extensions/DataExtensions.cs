using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EStore.BL.Models;
using EStore.BL.Models.Product;
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

        public static List<ProductCategoryItem> AllCategoriesFlatten(this EStoreEntities context)
        {
            return AllCategoriesHierarchy(context).Flatten(x => x.ChildCategories).ToList();
        }

        public static List<ProductCategoryItem> AllCategoriesHierarchy(this EStoreEntities context)
        {
            var categoryItems = context.Set<tblProductCategory>()
                .Include(x => x.tblProductCategory1)
                .Include(x => x.tblProductCategory2)
                .Where(x => x.ParentCategoryId == null)
                .ToList()
                .Select(x => new ProductCategoryItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    ChildCategories = x.tblProductCategory1
                        .Select(c => new ProductCategoryItem
                        {
                            Id = c.Id,
                            Name = c.Name,
                            ParentCategoryId = c.ParentCategoryId,
                            ParentCategoryName = c.ParentCategoryId != null ? c.tblProductCategory2.Name : "",
                        })
                        .ToList()
                })
                .ToList();

            return categoryItems;
        }
    }
}