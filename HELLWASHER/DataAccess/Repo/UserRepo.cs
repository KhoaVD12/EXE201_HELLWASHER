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
    public class UserRepo: BaseRepo<User>, IUserRepo
    {
        private readonly WashShopContext _context;
        public UserRepo(WashShopContext context): base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUserWithCart()
        {
            return await _context.Users.Include(e=>e.Cart).ThenInclude(e=>e.CartItems).ToListAsync();
        }
    }
}
