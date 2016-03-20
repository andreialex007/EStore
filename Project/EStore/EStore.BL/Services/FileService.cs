using System.Linq;
using EStore.BL.Models.Product;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class FileService : ServiceBase
    {
        public FileService(EStoreEntities entities) : base(entities)
        {
        }

        public FileItem AddFile(string path, string description, long? productId = null, long? supplierInvoiceId = null)
        {
            var item = new FileItem
            {
                Description = description,
                Path = path,
                ProductId = productId,
                SupplierInvoiceId = supplierInvoiceId
            };
            AddFile(item);
            return item;
        }

        public void SaveFileDescription(long id, string text)
        {
            Db.Set<tblFile>().Single(x => x.Id == id).Description = text;
            Db.SaveChanges();
        }

        public void AddFile(FileItem item)
        {
            var tblFile = new tblFile
            {
                Description = item.Description,
                Path = item.Path,
                ProductId = item.ProductId,
                SupplierInvoiceId = item.SupplierInvoiceId
            };

            Db.Set<tblFile>().Add(tblFile);
            Db.SaveChanges();

            item.Id = tblFile.Id;
        }
    }
}