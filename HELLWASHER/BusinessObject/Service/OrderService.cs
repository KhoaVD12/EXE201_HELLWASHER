using AutoMapper;
using BusinessObject.IService;
using BusinessObject.Utils;
using BusinessObject.ViewModels.OrderDTO;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using DataAccess;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using DataAccess.Enum;
using DataAccess.IRepo;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using BusinessObject.ViewModels.Order;

namespace BusinessObject.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepo<User> _userRepo;
        private readonly IBaseRepo<Order> _orderRepo;
        private readonly IBaseRepo<ServiceCheckout> _serviceCheckoutRepo;
        private readonly IBaseRepo<ProductCheckout> _productCheckoutRepo;
        private readonly IOrderRepo _orderRepository;
        private readonly IMapper _mapper;
        private readonly WashShopContext _dbcontext;
        private readonly Cloudinary _cloudinary;

        public OrderService(IMapper mapper, WashShopContext dbcontext, IBaseRepo<User> userRepo, IBaseRepo<Order> orderRepo, IBaseRepo<ServiceCheckout> serviceCheckoutRepo, IBaseRepo<ProductCheckout> productCheckoutRepo, IOrderRepo orderRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _orderRepo = orderRepo ?? throw new ArgumentNullException(nameof(orderRepo));
            _serviceCheckoutRepo = serviceCheckoutRepo;
            _productCheckoutRepo = productCheckoutRepo;
            _orderRepository = orderRepository;
        }
        public async Task<ServiceResponse<IEnumerable<OrderResponse>>> GetAllOrder(User user)
        {
            var response = new ServiceResponse<IEnumerable<OrderResponse>>();
            try
            {
                if (user == null || user.UserId <= 0)
                {
                    response.Success = false;
                    response.Message = "Invalid user.";
                    return response;
                }

                IEnumerable<Order> orders;
                if (user.Role == "Admin" || user.Role == "Staff")
                {
                    // Fetch all orders for Admin
                    orders = await _orderRepo.GetAllAsync();
                }
                else if (user.Role == "Customer")
                {
                    // Fetch orders for the specific customer
                    var userEntity = await _userRepo.GetByIdAsync(user.UserId);
                    orders = await _orderRepo.GetAllByConditionAsync(o => o.UserId == user.UserId);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Unauthorized role.";
                    return response;
                }

                // Map orders to OrderResponse
                var orderResponses = _mapper.Map<IEnumerable<OrderResponse>>(orders);
                response.Data = orderResponses;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
            }
            return response;
        }



        public async Task<ServiceResponse<AddOrderResponse>> AddOrder(QuickOrderDTO order, User user)
        {
            var serviceResponse = new ServiceResponse<AddOrderResponse>();

            try
            {
                //if (user == null || user.UserId <= 0)
                //{
                //    serviceResponse.Success = false;
                //    serviceResponse.Message = "Invalid user.";
                //    return serviceResponse;
                //}

                // Map DTO to entity
                var orderEntity = _mapper.Map<Order>(order);

                //var userEntity = await _userRepo.GetByIdAsync(user.UserId);
                //if (userEntity == null)
                //{
                //    serviceResponse.Success = false;
                //    serviceResponse.Message = "User not found.";
                //    return serviceResponse;
                //}

                orderEntity.UserId = user?.UserId;
                orderEntity.OrderStatus = OrderEnum.PENDING;
                orderEntity.OrderDate = DateTime.Now;
                orderEntity.WashStatus = WashEnum.PENDING;
                orderEntity.CusomterPhone = order.CusomterPhone;
                orderEntity.CustomerEmail = order.CustomerEmail;
                orderEntity.CustomerName = order.CustomerName;
                orderEntity.Address = order.Address;
                orderEntity.TotalPrice = 0;

                await _orderRepo.AddAsync(orderEntity);

                // Prepare success response
                serviceResponse.Data = new AddOrderResponse
                {
                    OrderId = orderEntity.OrderId,
                    Order = order
                };
                serviceResponse.Success = true;
                serviceResponse.Message = "Order created successfully!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"{ex.Message}";
            }

            return serviceResponse;
        }



        public async Task<ServiceResponse<UpdateOrderRequest>> UpdateOrder(UpdateOrderRequest orderRequest, int orderId, User user)
        {
            var response = new ServiceResponse<UpdateOrderRequest>();
            try
            {
                if (user == null || user.UserId <= 0)
                {
                    response.Success = false;
                    response.Message = "Invalid user.";
                    return response;
                }

                var order = await _orderRepo.GetByIdAsync(orderId);
                if (order == null || order.UserId != user.UserId)
                {
                    response.Success = false;
                    response.Message = "Order not found or does not belong to the user.";
                    return response;
                }

                var userEntity = await _userRepo.GetByIdAsync(user.UserId);
                if (userEntity != null)
                {
                    order.CustomerEmail = userEntity.Email;
                    order.CusomterPhone = userEntity.Phone;
                    order.CustomerName = userEntity.Name;
                }

                var orderDetails = await _orderRepository.GetOrderWithDetails(orderId);
                decimal totalServiceCheckout = order.ServiceCheckouts?.Sum(x => x.TotalPricePerService) ?? 0;
                decimal totalProductCheckout = order.ProductCheckouts?.Sum(x => x.TotalPricePerProduct) ?? 0;
                decimal totalPrice = totalServiceCheckout + totalProductCheckout;

                order.TotalPrice = totalPrice;
                order.OrderStatus = OrderEnum.UPDATED;
                order.Address = orderRequest.Address;
                order.WashStatus = WashEnum.PENDING;
                order.PickUpDate = orderRequest.PickUpDate;

                await _orderRepo.UpdateAsync(order);

                response.Success = true;
                response.Message = "Order updated successfully.";
                response.Data = orderRequest;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
            }
            return response;
        }


        public async Task<ServiceResponse<OrderResponse>> GetOrderById(int orderId, User user)
        {
            var response = new ServiceResponse<OrderResponse>();
            try
            {
                if (user == null || user.UserId <= 0 || orderId <= 0)
                {
                    response.Success = false;
                    response.Message = "Invalid user or order ID.";
                    return response;
                }

                // Fetch the order by orderId
                var order = await _orderRepo.GetByIdAsync(orderId);
                if (order == null || order.UserId != user.UserId)
                {
                    response.Success = false;
                    response.Message = "Order not found or does not belong to the user.";
                    return response;
                }

                // Map the order to OrderResponse
                var orderResponse = _mapper.Map<OrderResponse>(order);
                response.Data = orderResponse;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<OrderStatusRequest>> UpdateOrderStatus(int orderId, OrderStatusEnumRequest status)
        {
            var response = new ServiceResponse<OrderStatusRequest>();
            try
            {
                var order = await _orderRepo.GetByIdAsync(orderId);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order not found.";
                    return response;
                }
                if (order.ConfirmImage == null)
                {
                    response.Success = false;
                    response.Message = "Please upload confirm image.";
                    return response;
                }
                if (order.OrderStatus == OrderEnum.CONFIRMED)
                {
                    response.Success = false;
                    response.Message = "Order has already been confirmed.";
                    return response;
                }
                if (order.OrderStatus == OrderEnum.CANCELLED)
                {
                    response.Success = false;
                    response.Message = "Order has already been cancelled.";
                    return response;
                }
                order.OrderStatus = (OrderEnum)status;
                await _orderRepo.UpdateAsync(order);
                response.Data = new OrderStatusRequest
                {
                    OrderId = orderId,
                    Status = (OrderEnum)status
                };
                response.Success = true;
                response.Message = "Order Update succesfully.";
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }

        public async Task<ServiceResponse<bool>> SendConfirmOrderEmail(int orderId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var orderTask = _orderRepo.GetByIdAsync(orderId);

                var order = await orderTask;
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order not found.";
                    return response;
                }
                //if (order.ConfirmImage == null)
                //{
                //    response.Success = false;
                //    response.Message = "Please upload confirm image.";
                //    return response;
                //}
                //if (order.OrderStatus != OrderEnum.CONFIRMED || order.OrderStatus == null)
                //{
                //    response.Success = false;
                //    response.Message = "Order needs to be confirmed.";
                //    return response;
                //}

                var userItem = await _orderRepository.GetOrderWithDetails(orderId);

                decimal totalServiceCheckout = order.ServiceCheckouts?.Sum(x => x.TotalPricePerService) ?? 0;
                decimal totalProductCheckout = order.ProductCheckouts?.Sum(x => x.TotalPricePerProduct) ?? 0;
                decimal totalPrice = totalServiceCheckout + totalProductCheckout;
                if (order.UserId == null) 
                {
                    var orderEmailDTO = new ShowOrderEmailDTO
                    {
                        CusomterPhone = order.CusomterPhone,
                        CustomerEmail = order.CustomerEmail,
                        CustomerName = order.CustomerName,
                        Address = order.Address,
                        OrderDate = order.OrderDate,
                        PickUpDate = order.PickUpDate,
                        TotalPrice = totalPrice,
                        ServiceCheckouts = order.ServiceCheckouts,
                        ProductCheckouts = order.ProductCheckouts,
                        TotalService = totalServiceCheckout,
                        TotalProduct = totalProductCheckout
                    };
                    var userEmail = order.CustomerEmail;
                    if (!string.IsNullOrEmpty(userEmail))
                    {
                        var emailSent = await Utils.SendEmail.SendOrderEmail(orderEmailDTO, userEmail);
                    }
                }
                else
                {
                    var user = await _userRepo.GetByIdAsync((int)order.UserId);
                    var orderEmailDTO = new ShowOrderEmailDTO
                    {
                        CustomerName = user.Name,
                        CustomerEmail = user.Email,
                        CusomterPhone = user.Phone,
                        Address = order.Address,
                        OrderDate = order.OrderDate,
                        ServiceCheckouts = order.ServiceCheckouts,
                        ProductCheckouts = order.ProductCheckouts,
                        TotalService = totalServiceCheckout,
                        TotalProduct = totalProductCheckout,
                        PickUpDate = order.PickUpDate,
                        TotalPrice = totalPrice
                    };
                    var userEmail = user.Email;
                    if (!string.IsNullOrEmpty(userEmail))
                    {
                        var emailSent = await Utils.SendEmail.SendOrderEmail(orderEmailDTO, userEmail);
                    }
                }
                


                response.Success = true;
                response.Message = "Email sent successfully.";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }

        public async Task<ServiceResponse<bool>> AddConfirmImage(int orderId, IFormFile image)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var order = await _orderRepo.GetByIdAsync(orderId);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order not found.";
                    return response;
                }

                var imageService = new ImageService();
                string uploadedImageUrl = string.Empty;
                if (image != null)
                {
                    using (var stream = image.OpenReadStream())
                    {
                        uploadedImageUrl = await imageService.UploadImageAsync(stream, image.FileName);
                    }
                }
                if (!string.IsNullOrEmpty(image.ToString()) && Uri.IsWellFormedUriString(image.ToString(), UriKind.Absolute))
                {
                    uploadedImageUrl = await imageService.UploadImageFromUrlAsync(image.ToString());
                }

                order.ConfirmImage = image.ToString();
                await _orderRepo.UpdateAsync(order);
                response.Data = true;
                response.Success = true;
                response.Message = "Image uploaded succesfully.";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }

        public async Task<ServiceResponse<QuickOrderDTO>> QuickAddOrder(QuickOrderDTO orderDTO)
        {
            var response = new ServiceResponse<QuickOrderDTO>();
            try
            {
                var order = _mapper.Map<Order>(orderDTO);
                order.OrderStatus = OrderEnum.PENDING;
                order.OrderDate = DateTime.Now;
                order.WashStatus = WashEnum.PENDING;
                order.TotalPrice = 0;
                order.CusomterPhone = orderDTO.CusomterPhone;
                order.CustomerEmail = orderDTO.CustomerEmail;
                order.CustomerName = orderDTO.CustomerName;
                order.Address = orderDTO.Address;
                await _orderRepo.AddAsync(order);

                response.Data = orderDTO;
                response.Success = true;
                response.Message = "Order created successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }
    }
}