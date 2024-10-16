using DataAccess.Entity;
using DataAccess.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly WashShopContext _dbContext;
        public ProductRepo(WashShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> GetProductWithDetails(int id)
        {
            return await _dbContext.Products
                .Include(o => o.Category)
                .FirstOrDefaultAsync(o => o.ProductId == id);
        }
    }
}
