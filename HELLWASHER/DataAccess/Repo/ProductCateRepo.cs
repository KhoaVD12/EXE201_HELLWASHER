using DataAccess.BaseRepo;
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
    public class ProductCateRepo:BaseRepo<ProductCategory>, IProductCateRepo
    {
        private readonly WashShopContext _context;
        public ProductCateRepo(WashShopContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await _context.ProductCategories.Include(s => s.Products).ToListAsync();
        }
        public async Task<ProductCategory> GetById(int id)
        {
            return await _context.ProductCategories.Include(s => s.Products).FirstOrDefaultAsync(s => s.ProductCategoryId == id);
        }
    }
}
