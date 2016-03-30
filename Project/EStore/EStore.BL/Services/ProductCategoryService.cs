using System.Collections.Generic;
using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Models.Product;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class ProductCategoryService : ServiceBase
    {
        public ProductCategoryService(EStoreEntities entities) : base(entities)
        {
        }

        public List<ProductCategoryItem> All()
        {
            return Db.AllCategoriesFlatten();
        }

        public List<ProductCategoryItem> AllRoot()
        {
            return Db.AllCategoriesFlatten().Where(x => x.ParentCategoryId == null).ToList();
        }

        public void Save(ProductCategoryItem item)
        {
            var errors = item.GetValidationErrors();
            errors.ThrowIfHasErrors();

            var category = item.Id == 0 ? Db.CreateAndAdd<tblProductCategory>() : Db.Set<tblProductCategory>().Single(x => x.Id == item.Id);

            category.Name = item.Name;
            category.ParentCategoryId = item.ParentCategoryId;

            Db.SaveChanges();

            item.Id = category.Id;
        }

        public ProductCategoryItem Edit(long id)
        {
            var category = new ProductCategoryItem();
            if (id != 0)
            {
                category = Db.Set<tblProductCategory>()
                    .Select(x => new ProductCategoryItem
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ParentCategoryId = x.ParentCategoryId,
                        ParentCategoryName = x.ParentCategoryId != null ? x.tblProductCategory2.Name : ""
                    })
                    .Single(x => x.Id == id);
            }

            AppendData(category);

            return category;
        }

        public void AppendData(ProductCategoryItem item)
        {
            item.AvaliableCategories = item.Id == 0
                ? AllRoot()
                : AllRoot().Where(x => x.Id != item.Id).ToList();
        }
    }
}