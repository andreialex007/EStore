using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Models.Product;
using EStore.BL.Models._Common;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;
using Humanizer;

// ReSharper disable RedundantAssignment

namespace EStore.BL.Services
{
    public class ProductService : ServiceBase
    {
        public ProductService(EStoreEntities entities) : base(entities)
        {
        }

        public void Save(ProductItem item)
        {
            var errors = item.GetValidationErrors();

            errors.ThrowIfHasErrors();

            var product = new tblProduct();
            if (item.Id != 0)
            {
                product = Db.Set<tblProduct>().Single(x => x.Id == item.Id);
            }
            else
            {
                product = Db.Set<tblProduct>().Create();
                Db.Set<tblProduct>().Add(product);
            }

            product.Descripton = item.Descripton;
            product.Name = item.Name;
            product.YandexUrl = item.YandexUrl;
            product.Specs = item.Specs;
            product.CategoryId = item.CategoryId;

            Db.SaveChanges();

            item.Id = product.Id;
        }

        public ProductItem Get(long id)
        {
            var productItem = new ProductItem();
            var forSaleState = ProductSingleStateEnum.ForSale.CastTo<int>();
            if (id != 0)
            {
                productItem = Db.Set<tblProduct>()
                    .Where(x => x.Id == id)
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

                var firstImage = productItem.ProductImages.FirstOrDefault();
                if (firstImage != null && firstImage.Position > 0)
                {
                    firstImage.IsChecked = true;
                }
            }

            AppendData(productItem);

            return productItem;
        }

        public ProductListModel ByCategoryId(long categoryId)
        {
            var model = new ProductListModel();

            var forSaleState = ProductSingleStateEnum.ForSale.CastTo<int>();

            var items = Db.Set<tblProduct>()
                .Where(x => x.CategoryId == categoryId || x.tblProductCategory.ParentCategoryId == categoryId)
                .Select(x => new ProductItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    YandexUrl = x.YandexUrl,
                    Specs = x.Specs,
                    MainImage = x.tblFiles.OrderByDescending(f => f.Position).FirstOrDefault().Path,
                    Descripton = x.Descripton,
                    Price = x.tblProductSingles.FirstOrDefault(p => p.State == forSaleState).SellPrice ?? 0,
                    IsAvaliable = x.tblProductSingles.Any(p => p.State == forSaleState),
                    CategoryId = x.tblProductCategory.ParentCategoryId,
                    CategoryName = x.tblProductCategory.tblProductCategory2.Name,
                    SubCategoryId = x.CategoryId,
                    SubCategoryName = x.tblProductCategory.Name
                })
                .OrderBy(x => x.Name)
                .ToList();

            model.Products = items;


            var category = Db.Set<tblProductCategory>().Include(x => x.tblProductCategory2).Single(x => x.Id == categoryId);
            model.SubCategoryId = category.Id;
            model.SubCategoryName = category.Name;

            if (category.ParentCategoryId.HasValue)
            {
                model.CategoryName = category.tblProductCategory2.Name;
                model.CategoryId = category.ParentCategoryId;
            }

            return model;
        }

        public SearchModel<ProductItem> Search(
            string searchTerm,
            string orderBy,
            bool isAsc = false,
            int? take = null,
            int? skip = null)
        {
            searchTerm = (searchTerm ?? string.Empty).Trim().ToLower();
            var totalRecords = Db.Set<tblProduct>().Count();

            var query = Db.Set<tblProduct>()
                .Select(x => new ProductItem
                {
                    Id = x.Id,
                    Descripton = x.Descripton,
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    YandexUrl = x.YandexUrl,
                    Specs = x.Specs
                });

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x =>
                    x.Descripton.ToLower().Contains(searchTerm) ||
                    x.Specs.ToLower().Contains(searchTerm) ||
                    x.Name.ToLower().Contains(searchTerm));
            }

            var recordsFiltered = query.Count();
            var items = query
                .OrderBy(orderBy, isAsc)
                .TakePage(take, skip)
                .ToList();

            var model = new SearchModel<ProductItem>
            {
                recordsTotal = totalRecords,
                data = items,
                recordsFiltered = recordsFiltered
            };

            return model;
        }

        public void AppendData(ProductItem item)
        {
            item.AvaliableCategories = Db.AllCategoriesFlatten();
        }

    }
}