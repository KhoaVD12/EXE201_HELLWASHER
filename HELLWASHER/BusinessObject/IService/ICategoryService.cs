using BusinessObject.Model.Request;
using BusinessObject.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<ResponseCategoryDTO>> CreateCategory(CreateCategoryDTO categoryDTO);
        Task<ServiceResponse<bool>> DeleteCategory(int id);
        Task<ServiceResponse<PaginationModel<ResponseCategoryDTO>>> GetAllCategory(int page, int pageSize,
            string? search, string sort);
        Task<ServiceResponse<ResponseCategoryDTO>> UpdateCategory(int id, ResponseCategoryDTO categoryDTO);
    }
}
