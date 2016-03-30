using System;
using System.Linq;
using EStore.BL.Services._Common;
using EStore.DL;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class AppService : ServiceBase, IDisposable
    {
        public AppService(EStoreEntities entities) : base(entities)
        {
            Article = new ArticleService(entities);
            Product = new ProductService(entities);
            SupplierInvoice = new SupplierInvoiceService(entities);
            InvoicePosition = new SupplierInvoicePositionService(entities);
            File = new FileService(entities);
            ProductSingle = new ProductSingleService(entities);
            ProductCategory = new ProductCategoryService(entities);
            AdminUser = new AdminUserService(entities);
        }

        public ArticleService Article { get; set; }
        public ProductService Product { get; set; }
        public SupplierInvoiceService SupplierInvoice { get; set; }
        public SupplierInvoicePositionService InvoicePosition { get; set; }
        public ProductSingleService ProductSingle { get; set; }
        public FileService File { get; set; }
        public ProductCategoryService ProductCategory { get; set; }
        public AdminUserService AdminUser { get; set; }

        public void Dispose()
        {
            Db.Dispose();
        }

        public void Delete<T>(long id) where T : class, IdEntity
        {
            var element = Db.Set<T>().Single(x => x.Id == id);
            Db.Set<T>().Remove(element);
            Db.SaveChanges();
        }

        public string DeleteFile(long id)
        {
            var tblFile = Db.Set<tblFile>().Single(x => x.Id == id);
            var path = tblFile.Path;
            Db.Set<tblFile>().Remove(tblFile);
            Db.SaveChanges();
            return path;
        }
    }
}