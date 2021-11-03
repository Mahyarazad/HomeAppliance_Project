﻿using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Infrastructure
{
    public class RepositoryBase<TKey, T> : IRepositoty<TKey, T> where T : class
    {
        private readonly DbContext _dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T entity)
        {
            _dbContext.Add<T>(entity);
        }

        public bool Exist(System.Linq.Expressions.Expression<System.Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Any(expression);
        }

        public T Get(TKey id)
        {
            return _dbContext.Find<T>(id);
        }

        public System.Collections.Generic.List<T> GetList()
        {
            return _dbContext.Set<T>().ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}