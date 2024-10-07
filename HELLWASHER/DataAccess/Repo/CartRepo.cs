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
    public class CartRepo:BaseRepo<Cart>, ICartRepo
    {
        private readonly WashShopContext _context;
        public CartRepo(WashShopContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cart>> GetAllCartAndItem()
        {
            return await _context.Carts.Include(c => c.CartItems).ToListAsync();
        }
        public async Task<Cart> GetCartAndItemInsideByCartId(int id)
        {
            return await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c=>c.CartId==id);
        }
    }
}
