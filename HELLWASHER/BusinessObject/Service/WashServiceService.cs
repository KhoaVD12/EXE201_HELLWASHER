﻿using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Model.Request.CreateRequest;
using BusinessObject.Model.Request.UpdateRequest.Entity;
using BusinessObject.Model.Response;
using BusinessObject.Utils;
using BusinessObject.ViewModels.ServiceDTO;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Service
{
    public class WashServiceService:IWashServiceService
    {
        private readonly IBaseRepo<DataAccess.Entity.Service> _baseRepo;
        private readonly IMapper _mapper;
        public WashServiceService(IBaseRepo<DataAccess.Entity.Service> repo, IMapper mapper)
        {
            _baseRepo = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ResponseWashServiceDTO>> CreateWashService(CreateWashServiceDTO serviceDTO)
        {
            var res = new ServiceResponse<ResponseWashServiceDTO>();
            try
            {
                var existList = await _baseRepo.GetAllAsync();
                if(existList.Any(s=>s.Name==serviceDTO.Name))
                {
                    res.Success = false;
                    res.Message = "Name existed";
                    return res;
                }

                // Handle image upload (either local or from a link)
                var imageService = new ImageService();
                string uploadedImageUrl = string.Empty;

                if (serviceDTO.ImageFile != null)
                {
                    // Image is a local file uploaded via a form
                    using (var stream = serviceDTO.ImageFile.OpenReadStream())
                    {
                        uploadedImageUrl = await imageService.UploadImageAsync(stream, serviceDTO.ImageFile.FileName.ToString());
                    }
                }
                var mapp = _mapper.Map<DataAccess.Entity.Service>(serviceDTO);
                mapp.ServiceStatus = ServiceEnum.AVAILABLE;
                mapp.ImageURL=uploadedImageUrl;
                await _baseRepo.AddAsync(mapp);
                var result = _mapper.Map<ResponseWashServiceDTO>(mapp);
                res.Success = true;
                res.Data = result;
                res.Message = "Service Created Successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to create Service:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteWashService(int id)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var exist= await _baseRepo.GetByIdAsync(id);
                if (exist != null)
                {
                    await _baseRepo.DeleteAsync(id);
                    res.Success = true;
                    res.Message = "Delete Service Successfully";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Service not found";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to delete Service: {ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<PaginationModel<ResponseWashServiceDTO>>> GetAllWashService(int page, int pageSize, string? search, string sort)
        {
            var res = new ServiceResponse<PaginationModel<ResponseWashServiceDTO>>();
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
                    "price" => services.OrderBy(e => e.Price),
                    _ => services.OrderBy(e => e.ServiceId)
                };
                var mapp = _mapper.Map<IEnumerable<ResponseWashServiceDTO>>(services);
                if (mapp.Any())
                {
                    var paginationModel = await Pagination.GetPaginationEnum(mapp, page, pageSize);
                    res.Success = true;
                    res.Message = "Get Services successfully";
                    res.Data = paginationModel;
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Service";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to get Services:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseWashServiceDTO>> GetById(int id)
        {
            var res = new ServiceResponse<ResponseWashServiceDTO>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist != null)
                {
                    var result = _mapper.Map<ResponseWashServiceDTO>(exist);
                    res.Data = result;
                    res.Success = true;
                    res.Message = "Get Service Successfully";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Service with this ID";
                    return res;
                }
            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to get Service:{ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<ResponseWashServiceDTO>> UpdateWashService(int id, UpdateWashServiceDTO serviceDTO)
        {
            var res = new ServiceResponse<ResponseWashServiceDTO>();
            try
            {
                // Handle image upload (either local or from a link)
                var imageService = new ImageService();
                string uploadedImageUrl = string.Empty;

                if (serviceDTO.ImageFile != null)
                {
                    // Image is a local file uploaded via a form
                    using (var stream = serviceDTO.ImageFile.OpenReadStream())
                    {
                        uploadedImageUrl = await imageService.UploadImageAsync(stream, serviceDTO.ImageFile.FileName.ToString());
                    }
                }

                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No service found";
                    return res;
                }

                // Flag to track if anything was updated
                bool isUpdated = false;

                // Check each field for changes and update accordingly
                if (exist.Name != serviceDTO.Name)
                {
                    exist.Name = serviceDTO.Name;
                    isUpdated = true;
                }

                if (exist.Description != serviceDTO.Description)
                {
                    exist.Description = serviceDTO.Description;
                    isUpdated = true;
                }

                if (!string.IsNullOrEmpty(uploadedImageUrl) && exist.ImageURL != uploadedImageUrl)
                {
                    exist.ImageURL = uploadedImageUrl;
                    isUpdated = true;
                }

                if (exist.ClothUnit != serviceDTO.ClothUnit)
                {
                    exist.ClothUnit = serviceDTO.ClothUnit;
                    isUpdated = true;
                }

                if (exist.Price != serviceDTO.Price)
                {
                    exist.Price = serviceDTO.Price;
                    isUpdated = true;
                }

                // If nothing was changed, return a response indicating no changes were made
                if (!isUpdated)
                {
                    res.Success = false;
                    res.Message = "No changes were made to the service.";
                    return res;
                }

                // Otherwise, save the updated entity
                await _baseRepo.UpdateAsync(exist);

                // Return success response
                var result = _mapper.Map<ResponseWashServiceDTO>(exist); // Note: map the updated entity, not DTO
                res.Success = true;
                res.Message = "Service updated successfully";
                res.Data = result;
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to update Service: {ex.Message}";
                return res;
            }
        }

        public async Task<ServiceResponse<bool>> UpdateWashStatus(int id, ServiceEnum status)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var exist = await _baseRepo.GetByIdAsync(id);
                if (exist == null)
                {
                    res.Success = false;
                    res.Message = "No service found";
                    return res;
                }

                if (exist.ServiceStatus == status)
                {
                    res.Success = false;
                    res.Message = "No changes were made. Status is already set to the same value.";
                    return res;
                }

                exist.ServiceStatus = status;
                await _baseRepo.UpdateAsync(exist);
                res.Success = true;
                res.Message = "Update Successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Fail to update Service: {ex.Message}";
                return res;
            }
        }

    }
}
