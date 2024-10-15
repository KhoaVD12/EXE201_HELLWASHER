using DataAccess.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected DbSet<T> _dbSet;
        protected readonly WashShopContext context;
        public GenericRepo(WashShopContext context)
        {
            this.context = context;
            _dbSet = context.Set<T>();
        }
        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(T entity)
        {
            _ = await _dbSet.AddAsync(entity);
            _ = await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _ = _dbSet.Update(entity);
            _ = await context.SaveChangesAsync();
        }

        public async Task Remove(T entity)
        {
            _ = _dbSet.Remove(entity);
            _ = await context.SaveChangesAsync();
        }

        public void UpdateE(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
