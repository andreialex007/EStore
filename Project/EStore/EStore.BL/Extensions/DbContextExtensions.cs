using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using EStore.DL;
using EStore.DL.Mapping;

namespace EStore.BL.Extensions
{
    public static class DbContextExtensions
    {
        public static void AttachIfDetached<T>(this EStoreEntities db, T entity)
            where T : class
        {
            if (db.Entry(entity).State == EntityState.Detached)
                db.Set<T>().Attach(entity);
        }

        public static void Delete<T>(this EStoreEntities db, Expression<Func<T, bool>> expression) where T : class
        {
            var items = db.Set<T>().Where(expression);
            db.Set<T>().RemoveRange(items);
        }

        public static void AddOrCreateEntity<T>(this EStoreEntities db, T entity) where T : class, IdEntity
        {
            var entityState = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            db.Entry(entity).State = entityState;
            db.SaveChanges();
        }

        public static void AttachAndAdd<T>(this EStoreEntities db, T entity) where T : class, IdEntity
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Added;
        }

        public static void Save<T>(this EStoreEntities db, T entity) where T : class, IdEntity
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            db.SaveChanges();
        }

        public static void SetModifiedProperties<TEntity>(this DbEntityEntry<TEntity> entityEntry,
            params Expression<Func<TEntity, object>>[] properties) where TEntity : class, IdEntity
        {
            foreach (var property in properties)
            {
                entityEntry.Property(property).IsModified = true;
            }
        }

        public static void Attach<TEntity>(this EStoreEntities _db, TEntity entity) where TEntity : class
        {
            _db.Set<TEntity>().Attach(entity);
        }

        public static void Detach<TEntity>(this EStoreEntities _db, TEntity entity) where TEntity : class
        {
            _db.Entry(entity).State = EntityState.Detached;
        }
    }
}