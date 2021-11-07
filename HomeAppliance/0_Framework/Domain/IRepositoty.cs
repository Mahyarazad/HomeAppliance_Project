using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _0_Framework
{
    public interface IRepositoty<TKey, T> where T : class
    {
        T Get(TKey id);
        List<T> GetList();
        void Create(T entity);
        bool Exist(Expression<Func<T, bool>> expression);
        void SaveChanges();
    }
}