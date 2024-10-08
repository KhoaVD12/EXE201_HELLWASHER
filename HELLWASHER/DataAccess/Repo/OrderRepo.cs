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

        public async Task AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);

        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return orders;
        }
    }
}
