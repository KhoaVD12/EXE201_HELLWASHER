﻿using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Response;
using BusinessObject.Utils;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly IBaseRepo<Category> _baseRepo;
        private readonly IMapper _mapper;
        public CategoryService(IBaseRepo<Category> repo, IMapper mapper)
        {
            _baseRepo = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Category>> CreateCategory(CreateCategoryDTO categoryDTO)
        {
            var res = new ServiceResponse<Category>();
            try
            {
                var existList = await _baseRepo.GetAllAsync();
                if (existList.Any(s => s.Name == categoryDTO.Name))
                {
                    res.Success = false;
                    res.Message = "Name existed";
                    return res;
                }
                var mapp = _mapper.Map<Category>(categoryDTO);
                await _baseRepo.AddAsync(mapp);
                var result = _mapper.Map<ResponseCategoryDTO>(mapp);
                res.Success = true;
                res.Data = mapp;
                res.Message = "Category Created Successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to create Category:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteCategory(int id)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist != null)
                {
                    await _baseRepo.DeleteAsync(id);
                    res.Success = true;
                    res.Message = "Delete Category Successfully";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Category not found";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to delete Category: {ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<PaginationModel<Category>>> GetAllCategory(int page, int pageSize, string? search, string sort)
        {
            var res = new ServiceResponse<PaginationModel<Category>>();
            try
            {
                var services = await _baseRepo.GetAllAsync();
                if (!string.IsNullOrEmpty(search))
                {
                    services = services.Where(e => e.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

                }
                services = sort.ToLower().Trim() switch
                {
                    "name" => services.OrderBy(e => e.Name),
                    _ => services.OrderBy(e => e.CategoryId)
                };
                var mapp = _mapper.Map<IEnumerable<Category>>(services);
                if (mapp.Any())
                {
                    var paginationModel = await Pagination.GetPaginationEnum(mapp, page, pageSize);
                    res.Success = true;
                    res.Message = "Get Category successfully";
                    res.Data = paginationModel;
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Category";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to get Category:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseCategoryDTO>> UpdateCategory(int id, ResponseCategoryDTO categoryDTO)
        {
            var res = new ServiceResponse<ResponseCategoryDTO>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No Category found";
                    return res;
                }
                else
                {
                    exist.Name = categoryDTO.Name;
                    exist.Description = categoryDTO.Description;
                    
                    await _baseRepo.UpdateAsync(exist);
                    res.Success = true;
                    res.Message = "Update Category Successfully";
                    res.Data = categoryDTO;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to update Category:{ex.Message}";
                return res;
            }
        }
    }
}
