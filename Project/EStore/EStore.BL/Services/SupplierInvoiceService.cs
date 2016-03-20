using System.Data.Entity;
using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models.Product;
using EStore.BL.Models.SupplierInvoice;
using EStore.BL.Models._Common;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class SupplierInvoiceService : ServiceBase
    {
        public SupplierInvoiceService(EStoreEntities entities) : base(entities)
        {
        }

        public SupplierInvoiceItem Edit(long id)
        {
            var item = new SupplierInvoiceItem();
            if (id != 0)
            {
                item = Db.Set<tblSupplierInvoice>()
                    .Include(x => x.tblSupplierInvoicePositions)
                    .Where(x => x.Id == id)
                    .Select(x => new SupplierInvoiceItem
                    {
                        Id = x.Id,
                        BuyDate = x.BuyDate,
                        Notes = x.Notes,
                        SupplierId = x.SupplierId,
                        SupplierNumber = x.SupplierNumber,
                        Positions = x.tblSupplierInvoicePositions
                            .Select(p => new SupplierInvoicePositionItem
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName = p.tblProduct.Name,
                                Note = p.Note,
                                Price = p.Price,
                                Qty = p.Qty,
                                SupplierInvoiceId = p.SupplierInvoiceId
                            })
                            .ToList(),
                        Files = x.tblFiles
                            .Select(f => new FileItem
                            {
                                Id = f.Id,
                                Path = f.Path,
                                ProductId = f.ProductId,
                                SupplierInvoiceId = f.SupplierInvoiceId,
                                Description = f.Description
                            })
                            .ToList()
                    })
                    .Single();
            }

            AppendData(item);

            return item;
        }

        public SearchModel<SupplierInvoiceItem> Search(
            string searchTerm,
            string orderBy,
            bool isAsc = false,
            int? take = null,
            int? skip = null)
        {
            searchTerm = (searchTerm ?? string.Empty).Trim().ToLower();

            var query = Db.Set<tblSupplierInvoice>()
                .Select(x => new SupplierInvoiceItem
                {
                    Id = x.Id,
                    Total = x.tblSupplierInvoicePositions.Any() ? x.tblSupplierInvoicePositions.Sum(p => (p.Qty ?? 0) * (p.Price ?? 0)) : 0,
                    PositionsQty = x.tblSupplierInvoicePositions.Count,
                    BuyDate = x.BuyDate,
                    Notes = x.Notes,
                    SupplierId = x.SupplierId,
                    SupplierNumber = x.SupplierNumber
                });


            if (searchTerm.IsNotEmptyOrWhiteSpace())
            {
                query = query.Where(x => x.Notes.ToLower().Contains(searchTerm));
            }

            var recordsFiltered = query.Count();

            var items = query
                .OrderBy(orderBy, isAsc)
                .TakePage(take, skip)
                .ToList();

            var model = new SearchModel<SupplierInvoiceItem>
            {
                recordsTotal = Db.Set<tblSupplierInvoice>().Count(),
                data = items,
                recordsFiltered = recordsFiltered
            };

            return model;
        }

        public void AppendData(SupplierInvoiceItem item)
        {
            item.AvaliableSuppliers = Db.AllSuppliers();
        }

        public void Save(SupplierInvoiceItem item)
        {
            var errors = item.GetValidationErrors();
            errors.ThrowIfHasErrors();

            var invoice = new tblSupplierInvoice();
            if (item.Id != 0)
            {
                invoice = Db.Set<tblSupplierInvoice>().Single(x => x.Id == item.Id);
            }
            else
            {
                invoice = Db.CreateAndAdd<tblSupplierInvoice>();
            }

            invoice.BuyDate = item.BuyDate;
            invoice.Notes = item.Notes;
            invoice.SupplierId = item.SupplierId;
            invoice.SupplierNumber = item.SupplierNumber;

            Db.SaveChanges();

            item.Id = invoice.Id;
        }
    }
}