using DK.Dal.Entities.Base;
using DK.Dal.Interfaces;
using NHibernate;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DK.Dal.Repositories
{
    public class CoreRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly ISession _session;

        public CoreRepository(ISession session)
        {
            _session = session;
        }

        #region Implementation of IRepository<TEntity>

        public TEntity Get(int id)
        {
            return _session.Get<TEntity>(id);
        }

        public int Add(TEntity entity)
        {
            _session.Save(entity);

            return entity.Id;
        }

        public void Delete(TEntity entity)
        {
            _session.Delete(entity);
            _session.Flush();
        }

        public void Update(TEntity entity)
        {
            _session.Update(entity);
            _session.Flush();
        }

        public IQueryable<TEntity> Items
        {
            get { return _session.Query<TEntity>(); }
        }

        public TEntity FindBy(Expression<Func<TEntity, bool>> expression)
        {
            return FilterBy(expression).Single();
        }

        public TEntity FindBy(int id)
        {
            return _session.Get<TEntity>(id);
        }

        public IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression)
        {
            return Items.Where(expression).AsQueryable();
        }

        #endregion
    }
}