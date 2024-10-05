using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface ICartService
    {
        Task<ServiceResponse<IEnumerable<ResponseCartDTO>>> GetCarts();

    }
}
