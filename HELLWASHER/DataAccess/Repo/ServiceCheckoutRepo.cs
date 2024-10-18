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
    public class ServiceCheckoutRepo:BaseRepo<ServiceCheckout>, IServiceCheckoutRepo
    {
        private readonly WashShopContext _context;
        public ServiceCheckoutRepo(WashShopContext context):base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceCheckout>> GetAll()
        {
            return await _context.ServiceCheckouts.Include(s => s.Service).ToListAsync();
        }
    }
}
