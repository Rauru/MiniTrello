using System;
using System.Linq;
using System.Linq.Expressions;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;
using NHibernate;
using NHibernate.Linq;

namespace MiniTrello.Data
{
    public class ReadOnlyRepository : IReadOnlyRepository
    {
        private readonly ISession _session;

        public ReadOnlyRepository(ISession session)
        {
            _session = session;
        }

        public T First<T>(Expression<Func<T, bool>> query) where T : class, IEntity
        {
            T firstOrDefault = _session.Query<T>().FirstOrDefault(query);
            return firstOrDefault;
        }

        public T GetById<T>(long id) where T : class, IEntity
        {
            var item = _session.Get<T>(id);
            return item;
        }

        public T Getbyemail<T>(string email) where T : class, IEntity
        {
            //throw new NotImplementedException();
            var item = _session.Get<T>(email);
            return item;
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class, IEntity
        {
            return _session.Query<T>().Where(expression);
        }

        public T Create<T>(T itemToCreate) where T : class, IEntity
        {
            _session.Save(itemToCreate);
            return itemToCreate;
        }

        public T Update<T>(T itemToUpdate) where T : class, IEntity
        {
            _session.Update(itemToUpdate);
            return itemToUpdate;
        }

        public void Delete<T>(T itemtoDelete)
        {
            _session.Delete(itemtoDelete);
        }
    }
}