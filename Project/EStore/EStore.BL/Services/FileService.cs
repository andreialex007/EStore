using System.Data.Entity;
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

        public FileItem AddFile(string path, string description, long? productId = null, long? supplierInvoiceId = null, decimal position = 0)
        {
            var item = new FileItem
            {
                Description = description,
                Path = path,
                ProductId = productId,
                SupplierInvoiceId = supplierInvoiceId,
                Position = position
            };
            AddFile(item);
            return item;
        }

        public void SaveFileDescription(long id, string text)
        {
            Db.Set<tblFile>().Single(x => x.Id == id).Description = text;
            Db.SaveChanges();
        }

        public void SaveFileFirstPosition(long id)
        {
            var productFiles = Db.Set<tblProduct>()
                .Include(x => x.tblFiles)
                .Single(x => x.tblFiles.Any(f => f.Id == id))
                .tblFiles
                .ToList();

            productFiles.ForEach(x => x.Position = 0);
            productFiles.Single(x => x.Id == id).Position = 1;

            Db.SaveChanges();
        }

        public void AddFile(FileItem item)
        {
            var tblFile = new tblFile
            {
                Description = item.Description,
                Path = item.Path,
                ProductId = item.ProductId,
                SupplierInvoiceId = item.SupplierInvoiceId,
                Position = item.Position
            };

            Db.Set<tblFile>().Add(tblFile);
            Db.SaveChanges();

            item.Id = tblFile.Id;
        }
    }
}