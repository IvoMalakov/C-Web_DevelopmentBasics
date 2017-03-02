using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShouterApp.Data.Contracts
{
    public interface IRepository<T>
    {
        void Insert(T entity);

        void Delete(T entity);

        IQueryable<T> GetAll();

        T GetById(int id);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        T FindByPredicate(Expression<Func<T, bool>> predicate);
    }
}
