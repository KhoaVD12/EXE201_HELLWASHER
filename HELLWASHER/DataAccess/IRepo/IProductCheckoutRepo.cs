using DataAccess.BaseRepo;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepo
{
    public interface IProductCheckoutRepo : IBaseRepo<ProductCheckout>
    {
        Task<IEnumerable<ProductCheckout>> GetAll();
    }
}
