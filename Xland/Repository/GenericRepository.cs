using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xland.Repository;
using Xland.DAL;
using Xland.UnitOfWork;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Xland.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected XlandContext DbContext;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this.DbContext = unitOfWork.DbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = this.DbContext.Set<TEntity>();
            return query;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = this.DbContext.Set<TEntity>().Where(predicate);
            return query;
        }

        public TEntity Add(TEntity entity)
        {
            return DbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Edit(TEntity entity)
        {
            DbContext.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Attach(TEntity entity)
        {
            DbContext.Set<TEntity>().Attach(entity);
        }

        public TEntity Find(int? id)
        {
            return this.DbContext.Set<TEntity>().Find(id);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbContext.Set<TEntity>().Count(predicate);
        }

        public void DeleteMany(IEnumerable<TEntity> entities)
        {
            this.DbContext.Set<TEntity>().RemoveRange(entities);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}