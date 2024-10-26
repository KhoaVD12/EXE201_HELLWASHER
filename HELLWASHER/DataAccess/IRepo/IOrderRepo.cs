using DataAccess.Entity;
using DataAccess.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepo
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        Task<Order> GetOrderWithDetails(int id);
        Task<List<Order>> GetOrdersByUser(int userId);
    }
}
