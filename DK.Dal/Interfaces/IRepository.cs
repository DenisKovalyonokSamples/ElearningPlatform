using DK.Dal.Entities.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DK.Dal.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Get(int id);
        int Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        TEntity FindBy(Expression<Func<TEntity, bool>> expression);
        TEntity FindBy(int id);
        IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Items { get; }
    }
}
