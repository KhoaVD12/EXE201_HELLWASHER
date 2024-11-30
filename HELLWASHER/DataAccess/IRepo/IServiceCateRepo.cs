using DataAccess.BaseRepo;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepo
{
    public interface IServiceCateRepo:IBaseRepo<ServiceCategory>
    {
        Task<IEnumerable<ServiceCategory>> GetAll();
        Task<ServiceCategory> GetById(int id);
    }
}
