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
    public class ServiceCateRepo:BaseRepo<ServiceCategory>, IServiceCateRepo
    {
        private readonly WashShopContext _context;
        public ServiceCateRepo(WashShopContext context):base(context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<ServiceCategory>> GetAll()
        {
            return await _context.ServiceCategories.Include(s=>s.Services).ToListAsync();
        }
        public async Task<ServiceCategory> GetById(int id)
        {
            return await _context.ServiceCategories.Include(s => s.Services).FirstOrDefaultAsync(s => s.ServiceCategoryId == id);
        }
    }
}
