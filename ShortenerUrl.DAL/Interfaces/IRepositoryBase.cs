﻿using System.Linq.Expressions;

namespace ShortenerUrl.DAL.Interfaces
{
    public  interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);

        


    }
}
