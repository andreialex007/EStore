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
        }

        public ArticleService Article { get; set; }

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
    }
}