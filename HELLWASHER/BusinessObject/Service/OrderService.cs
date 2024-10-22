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
        public async Task<ServiceResponse<IEnumerable<Order>>> GetAllOrder()
        {
            var response = new ServiceResponse<IEnumerable<Order>>();

            try
            {
                var orders = await _orderRepo.GetAllAsync();


                var orderDtOs = _mapper.Map<IEnumerable<OrderDTO>>(orders); // Map orders to OrderDTO
                response.Data = orders;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<AddOrderResponse>> AddOrder(OrderDTO order, int userId)
        {
            var serviceResponse = new ServiceResponse<AddOrderResponse>();

            try
            {
                // Map DTO to entity
                var OrderEntity = _mapper.Map<Order>(order);

                var user = await _userRepo.GetByIdAsync(userId);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found.";
                    return serviceResponse;
                }
                OrderEntity.UserId = userId;
                OrderEntity.OrderStatus = OrderEnum.PENDING;
                OrderEntity.OrderDate = DateTime.Now;
                OrderEntity.WashStatus = WashEnum.PENDING;
                OrderEntity.CusomterPhone = user.Phone;
                OrderEntity.CustomerEmail = user.Email;
                OrderEntity.CustomerName = user.Name;
                OrderEntity.Address = order.Address;
                OrderEntity.TotalPrice = 0;

                await _orderRepo.AddAsync(OrderEntity);
                // Prepare success response
                serviceResponse.Data = new AddOrderResponse
                {
                    OrderId = OrderEntity.OrderId,
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


        public async Task<ServiceResponse<UpdateOrderRequest>> UpdateOrder(UpdateOrderRequest orderRequest, int orderId)
        {
            var response = new ServiceResponse<UpdateOrderRequest>();
            try
            {
                var order = await _orderRepo.GetByIdAsync(orderId);
                if (order.UserId != null)
                {
                    var user = await _userRepo.GetByIdAsync((int)order.UserId);
                    order.CustomerEmail = user.Email;
                    order.CusomterPhone = user.Phone;
                    order.CustomerName = user.Name;
                }
                var orderDetails = await _orderRepository.GetOrderWithDetails(orderId);
                decimal totalServiceCheckout = order.ServiceCheckouts?.Sum(x => x.TotalPricePerService) ?? 0;
                decimal totalProductCheckout = order.ProductCheckouts?.Sum(x => x.TotalPricePerService) ?? 0;
                decimal totalPrice = totalServiceCheckout + totalProductCheckout;
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order not found.";
                    return response;
                }

                order.TotalPrice = totalPrice;
                order.OrderStatus = OrderEnum.UPDATED;
                order.Address = orderRequest.Address;
                order.WashStatus = WashEnum.PENDING;
                order.PickUpDate = orderRequest.PickUpDate;
                var imageService = new ImageService();
                string uploadedImageUrl = string.Empty;
                await _orderRepo.UpdateAsync(order);

                response.Success = true;
                response.Message = "Order updated successfully.";
                response.Data = orderRequest;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
        }

        public async Task<ServiceResponse<Order>> GetOrderById(int orderId)
        {
            var response = new ServiceResponse<Order>();
            try
            {
                var exist = await _orderRepo.GetByIdAsync(orderId);
                var orderDetails = await _orderRepository.GetOrderWithDetails(orderId);
                if (exist == null)
                {
                    response.Success = false;
                    response.Message = "Order not found.";
                    return response;
                }
                var order = _mapper.Map<Order>(exist);
                response.Data = order;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
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
                if (order.ConfirmImage == null)
                {
                    response.Success = false;
                    response.Message = "Please upload confirm image.";
                    return response;
                }
                if (order.OrderStatus != OrderEnum.CONFIRMED || order.OrderStatus == null)
                {
                    response.Success = false;
                    response.Message = "Order needs to be confirmed.";
                    return response;
                }

                var userItem = await _orderRepository.GetOrderWithDetails(orderId);

                decimal totalServiceCheckout = order.ServiceCheckouts?.Sum(x => x.TotalPricePerService) ?? 0;
                decimal totalProductCheckout = order.ProductCheckouts?.Sum(x => x.TotalPricePerService) ?? 0;
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
                        TotalPrice = order.TotalPrice,
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
                        TotalPrice = order.TotalPrice
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