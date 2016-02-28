using System;
using System.Linq;
using EStore.BL.Models;
using EStore.BL.Services._Common;
using EStore.DL;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class AppService : ServiceBase, IDisposable
    {
        public ArticleService Article { get; set; }
        public ProductService Product { get; set; }

        public AppService(EStoreEntities entities) : base(entities)
        {
            Article = new ArticleService(entities);
            Product = new ProductService(entities);
        }

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

        public string GetFilePath(long id)
        {
            return Db.Set<tblFile>().Single(x => x.Id == id).Path;
        }
    }
}