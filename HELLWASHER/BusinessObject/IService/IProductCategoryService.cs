﻿using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Response;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IService
{
    public interface IProductCategoryService
    {
        Task<ServiceResponse<ProductCategory>> CreateCategory(CreateCategoryDTO categoryDTO);
        Task<ServiceResponse<bool>> DeleteCategory(int id);
        Task<ServiceResponse<PaginationModel<ProductCategory>>> GetAllCategory(int page, int pageSize,
            string? search, string sort);
        Task<ServiceResponse<ProductCategory>> GetById(int id);
        Task<ServiceResponse<ResponseCategoryDTO>> UpdateCategory(int id, ResponseCategoryDTO categoryDTO);
    }
}