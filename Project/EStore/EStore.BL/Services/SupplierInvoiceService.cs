using System.Data.Entity;
using System.Linq;
using EStore.BL.Models.SupplierInvoice;
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
                        SupplierNumber = x.SupplierNumber,
                        Positions = x.tblSupplierInvoicePositions
                            .Select(p => new SupplierInvoicePositionItem
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                Note = p.Note,
                                Price = p.Price,
                                Qty = p.Qty,
                                SupplierInvoiceId = p.SupplierInvoiceId
                            })
                            .ToList()
                    })
                    .Single();
            }

            return item;
        }
    }
}