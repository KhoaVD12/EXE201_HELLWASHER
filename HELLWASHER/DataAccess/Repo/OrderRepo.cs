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
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        private readonly WashShopContext _dbContext;
        public OrderRepo(WashShopContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<Order>> GetOrdersByUser(int userId)
        {
            return await _dbContext.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order> GetOrderWithDetails(int id)
        {
            return await _dbContext.Orders
                .Include(o => o.ServiceCheckouts)
                .ThenInclude(sc => sc.Service)
                .Include(o => o.ProductCheckouts)
                .ThenInclude(sc => sc.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }
    }
}
