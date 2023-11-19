using Microsoft.EntityFrameworkCore;
using ShortenerUrl.DAL.Data;
using ShortenerUrl.DAL.Interfaces;
using System.Linq.Expressions;

namespace ShortenerUrl.DAL.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
       
        protected ApplicationDbContext context;

        public RepositoryBase(ApplicationDbContext context)
        {

            this.context = context;
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {     
               return !trackChanges ? context.Set<T>().AsNoTracking() : context.Set<T>();              
        }
      

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) 
        {                
                return !trackChanges ? context.Set<T>().Where(expression).AsNoTracking() : context.Set<T>().Where(expression);                       
        }
        

        public async Task CreateAsync(T entity) 
        {   
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();              
        }
        public async Task UpdateAsync(T entity) 
        {     
           context.Set<T>().Update(entity);
           await context.SaveChangesAsync();                        
        }
        public async Task DeleteAsync(T entity) 
        {     
           context.Set<T>().Remove(entity);
           await context.SaveChangesAsync();     
        } 


    }
}
