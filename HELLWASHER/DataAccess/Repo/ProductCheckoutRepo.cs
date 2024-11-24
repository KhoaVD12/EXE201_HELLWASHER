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
    public class ProductCheckoutRepo : BaseRepo<ProductCheckout>, IProductCheckoutRepo
    {
        private readonly WashShopContext _context;
        public ProductCheckoutRepo(WashShopContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductCheckout>> GetAll()
        {
            return await _context.ProductCheckouts.Include(s => s.Product).ToListAsync();
        }
    }
}
