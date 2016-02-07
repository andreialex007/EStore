using EStore.DL;
using EStore.DL.Mapping;

namespace EStore.BL.Services._Common
{
    public class InternalServiceBase<TEntity> :
        EntityServiceBase<TEntity> where TEntity : class, IdEntity, new()
    {
        public InternalServiceBase(EStoreEntities entities)
            : base(entities)
        {
        }
    }
}
