using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xland.Repository
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(int? id);
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        void DeleteMany(IEnumerable<TEntity> entities);
        void Edit(TEntity entity);

        void Attach(TEntity entity);

        int Count(Expression<Func<TEntity, bool>> predicate);

        void Dispose();

    }
}
