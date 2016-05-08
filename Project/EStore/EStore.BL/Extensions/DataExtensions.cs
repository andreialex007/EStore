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

        public static List<AdminUserItem> AllAdminUsers(this EStoreEntities context)
        {
            var items = context.Set<tblUser>()
                .Where(x => x.IsAdmin == true)
                .Select(x => new AdminUserItem
                {
                    Id = x.Id,
                    Email = x.Email,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                })
                .ToList();

            return items;
        }

        public static ProductItem GetProductItem(this EStoreEntities context, long productId)
        {
            var forSaleState = ProductSingleStateEnum.ForSale.CastTo<int>();

            var item = context.Set<tblProduct>()
                    .Where(x => x.Id == productId)
                    .Select(x => new ProductItem
                    {
                        Id = x.Id,
                        Descripton = x.Descripton,
                        CategoryId = x.CategoryId,
                        CategoryName = x.tblProductCategory.Name,
                        Name = x.Name,
                        YandexUrl = x.YandexUrl,
                        Specs = x.Specs,
                        Price = x.tblProductSingles.FirstOrDefault(p => p.State == forSaleState).SellPrice ?? 0,
                        IsAvaliable = x.tblProductSingles.Any(p => p.State == forSaleState),
                        MainImage = x.tblFiles.OrderByDescending(f => f.Position).FirstOrDefault().Path,
                        SubCategoryId = x.tblProductCategory.ParentCategoryId,
                        SubCategoryName = x.tblProductCategory.tblProductCategory2.Name,
                        ProductImages = x.tblFiles
                            .Select(f => new FileItem
                            {
                                Id = f.Id,
                                Description = f.Description,
                                Position = f.Position ?? 0,
                                Path = f.Path,
                                ProductId = f.ProductId
                            })
                            .OrderByDescending(f => f.Position)
                            .ToList(),
                        ProductSingleItems = x.tblProductSingles
                            .Select(s => new ProductSingleItem
                            {
                                Id = s.Id,
                                ProductId = s.ProductId,
                                BuyPrice = s.BuyPrice,
                                IsNew = s.IsNew,
                                OrderId = s.OrderId,
                                StateId = s.State,
                                SellPrice = s.SellPrice
                            })
                            .ToList(),
                        FeedbackItems = x.tblProductFeedbacks
                            .Select(f => new ProductFeedbackItem
                            {
                                Id = f.Id,
                                ProductId = f.ProductId,
                                UserName = f.UserName,
                                Minuses = f.Minuses,
                                Comment = f.Comment,
                                Stars = f.Stars,
                                Pluses = f.Pluses
                            })
                            .ToList()
                    })
                    .Single();

            return item;
        }
    }
}