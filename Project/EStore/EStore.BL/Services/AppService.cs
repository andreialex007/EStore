using System;
using EStore.BL.Services._Common;
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
    }
}