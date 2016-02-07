using EStore.BL.Extensions;
using EStore.DL;
using EStore.DL.Mapping;

namespace EStore.BL.Services._Common
{
    public class EntityServiceBase<TEntity> : ServiceBase where TEntity : class, IdEntity, new()
    {
        public EntityServiceBase(EStoreEntities entities)
            : base(entities)
        {
        }

        public virtual void Delete(long id)
        {
            var entity = new TEntity
                         {
                             Id = id
                         };
            Db.AttachIfDetached(entity);
            Db.Set<TEntity>().Remove(entity);
            Db.SaveChanges();
        }
    }
}