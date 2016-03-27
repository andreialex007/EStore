using System.Linq;
using EStore.BL.Extensions;
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
                                IsSelling = s.IsSelling,
                                OrderId = s.OrderId,
                                SellPrice = s.SellPrice
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

    }
}