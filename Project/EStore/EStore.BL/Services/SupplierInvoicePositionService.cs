using System.Data.Entity;
using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models.SupplierInvoice;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class SupplierInvoicePositionService : ServiceBase
    {
        public SupplierInvoicePositionService(EStoreEntities entities) : base(entities)
        {
        }

        public void Save(SupplierInvoicePositionItem item)
        {
            var errors = item.GetValidationErrors();
            errors.ThrowIfHasErrors();

            var position = new tblSupplierInvoicePosition();
            if (item.Id != 0)
            {
                position = Db.Set<tblSupplierInvoicePosition>()
                    .Include(x => x.tblSupplierInvoice)
                    .Single(x => x.Id == item.Id);
            }
            else
            {
                position = Db.CreateAndAdd<tblSupplierInvoicePosition>();
            }

            position.Note = item.Note;
            position.Price = item.Price;
            position.ProductId = item.ProductId;
            position.Qty = item.Qty;
            position.SupplierInvoiceId = item.SupplierInvoiceId;

            Db.SaveChanges();

            item.Id = position.Id;
        }

    }
}