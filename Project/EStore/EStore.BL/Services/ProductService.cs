using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Models.Product;
using EStore.BL.Models._Common;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;

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

            Db.SaveChanges();

            item.Id = product.Id;
        }

        public ProductItem Edit(long id)
        {
            var productItem = new ProductItem();
            if (id != 0)
            {
                productItem = Db.Set<tblProduct>()
                    .Where(x => x.Id == id)
                    .Select(x => new ProductItem
                    {
                        Id = x.Id,
                        Descripton = x.Descripton,
                        Name = x.Name,
                        ProductImages = x.tblFiles
                            .Select(f => new ProductImageItem
                            {
                                Id = f.Id,
                                Description = f.Description,
                                Path = f.Path,
                                ProductId = f.ProductId
                            })
                            .ToList(),
                        ProductSingleItems = x.tblProductSingles
                            .Select(s => new ProductSingleItem
                            {
                                Id = s.Id,
                                ProductId = s.ProductId,
                                BuyPrice = s.BuyPrice,
                                IsNew = s.IsNew,
                                IsSelling = s.IsSelling,
                                OrderId = s.OrderId,
                                SellPrice = s.SellPrice
                            }).ToList()
                    })
                    .Single();
            }

            return productItem;
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
                    Name = x.Name
                });

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x =>
                    x.Descripton.ToLower().Contains(searchTerm) ||
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
        }

        public ProductImageItem AddFile(string path, string description, long productId)
        {
            var item = new ProductImageItem
            {
                Description = description,
                Path = path,
                ProductId = productId
            };
            AddFile(item);
            return item;
        }

        public void SaveImageDescription(long id, string text)
        {
            Db.Set<tblFile>().Single(x => x.Id == id).Description = text;
            Db.SaveChanges();
        }

        public void AddFile(ProductImageItem item)
        {
            var tblFile = new tblFile
            {
                Description = item.Description,
                Path = item.Path,
                ProductId = item.ProductId
            };

            Db.Set<tblFile>().Add(tblFile);
            Db.SaveChanges();

            item.Id = tblFile.Id;
        }
    }
}