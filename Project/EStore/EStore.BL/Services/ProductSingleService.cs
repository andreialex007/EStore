using System;
using System.Data.Entity;
using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Models.Product;
using EStore.BL.Models._Common;
using EStore.BL.Services._Common;
using EStore.BL.Utils;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class ProductSingleService : ServiceBase
    {
        public ProductSingleService(EStoreEntities entities) : base(entities)
        {
        }

        public void Save(ProductSingleItem item)
        {
            var productSingle = Db.Set<tblProductSingle>().Single(x => x.Id == item.Id);

            productSingle.State = item.StateId;
            productSingle.BuyPrice = item.BuyPrice;
            productSingle.SellPrice = item.SellPrice;
            productSingle.IsNew = item.IsNew;
            productSingle.Notes = item.Notes;

            Db.SaveChanges();
        }

        public SearchModel<ProductSingleSearchItem> Search(
            long productId,
            string searchTerm,
            string orderBy,
            bool isAsc = false,
            int? take = null,
            int? skip = null)
        {
            searchTerm = (searchTerm ?? string.Empty).Trim().ToLower();
            var tblProductSinglesQuery = Db.Set<tblProductSingle>().Where(x => x.ProductId == productId);

            var stateOnHoldId = ProductSingleStateEnum.OnHold.CastTo<int>();
            var stateOnHoldName = ProductSingleStateEnum.OnHold.DescriptionAttr();

            var stateForSaleId = ProductSingleStateEnum.ForSale.CastTo<int>();
            var stateForSaleName = ProductSingleStateEnum.ForSale.DescriptionAttr();

            var query = tblProductSinglesQuery
                .Select(x => new ProductSingleSearchItem
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Notes = x.Notes,
                    SupplierInvoiceId = x.SupplierInvoiceId,
                    StateName = (x.State == stateOnHoldId ? stateOnHoldName : (x.State == stateForSaleId ? stateForSaleName : "")),
                    StateId = x.State,
                    IsNew = x.IsNew,
                    BuyPrice = x.BuyPrice,
                    SellPrice = x.SellPrice,
                    OrderId = x.OrderId,
                    Margin = Math.Round((decimal)(((x.SellPrice - x.BuyPrice) / x.BuyPrice) * 100), 2),
                    SupplierInvoicePositionId = x.SupplierInvoicePositionId
                });

            if (searchTerm.IsNotEmptyOrWhiteSpace())
                query = query.Where(x => x.Notes.ToLower().Contains(searchTerm));

            var recordsFiltered = query.Count();
            var items = query
                .OrderBy(orderBy, isAsc)
                .TakePage(take, skip)
                .ToList();

            var model = new SearchModel<ProductSingleSearchItem>
            {
                recordsTotal = tblProductSinglesQuery.Count(),
                data = items,
                recordsFiltered = recordsFiltered
            };

            return model;
        }

        public void GenerateProductSingles(long invoiceId)
        {
            var invoice = Db.Set<tblSupplierInvoice>()
                .Include(x => x.tblSupplierInvoicePositions)
                .Single(x => x.Id == invoiceId);

            foreach (var position in invoice.tblSupplierInvoicePositions)
            {
                for (int i = 0; i < (position.Qty ?? 0); i++)
                {
                    var single = Db.CreateAndAdd<tblProductSingle>();
                    single.BuyPrice = position.Price;
                    single.ProductId = position.ProductId;
                    single.SellPrice = position.Price;
                    single.State = ProductSingleStateEnum.ForSale.CastTo<int>();
                    single.IsNew = true;
                    single.SupplierInvoiceId = invoiceId;
                    single.SupplierInvoicePositionId = position.Id;
                }
            }

            Db.SaveChanges();
        }
    }
}