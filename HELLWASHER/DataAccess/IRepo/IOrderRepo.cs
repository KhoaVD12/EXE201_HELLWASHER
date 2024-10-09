﻿using DataAccess.Entity;
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
        Task AddOrder(Order order);
        Task<Order?> GetOrderWithDetailsAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrders();
    }
}