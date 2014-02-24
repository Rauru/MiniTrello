using System;
using System.Linq;
using System.Linq.Expressions;
using MiniTrello.Domain.Entities;

namespace MiniTrello.Domain.Services
{
    public interface IReadOnlyRepository
    {
        T First<T>(Expression<Func<T, bool>> query) where T : class, IEntity;
        T GetById<T>(long id) where T : class, IEntity;
        // GetByName<T>(string firstName) where T : class, IEntity;
        T Getbyemail<T>(string email) where T : class, IEntity;
        IQueryable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class, IEntity;

        //Account GetById<T1, T2>(global::MiniTrello.Api.Models.AccountRegisterModel model);
    }
}