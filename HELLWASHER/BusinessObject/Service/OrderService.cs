using AutoMapper;
using BusinessObject.IService;
using BusinessObject.ViewModels.OrderDTO;
using DataAccess;
using DataAccess.BaseRepo;
using DataAccess.Entity;
using DataAccess.Enum;
using DataAccess.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetAllOrder()
        {
            var response = new ServiceResponse<IEnumerable<OrderDTO>>();

            try
            {
                var orders = await _orderRepo.GetAllAsync();


                var orderDtOs = _mapper.Map<IEnumerable<OrderDTO>>(orders); // Map orders to OrderDTO
                response.Data = orderDtOs;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<OrderDTO>> AddOrder(OrderDTO order, int userId)
        {
            var serviceResponse = new ServiceResponse<OrderDTO>();

            try
            {
                // Map DTO to entity
                var OrderEntity = _mapper.Map<Order>(order);



                var user = await _userRepo.GetByIdAsync(userId);
                //var orderEmailDTO = new ShowOrderEmailDTO 
                //{
                //    UserName = user.Name,
                //    Address = OrderEntity.Address,
                //    ServiceCheckoutId = OrderEntity.ServiceCheckoutId,
                //    ProductCheckoutId = OrderEntity.ProductCheckoutId,
                //    PickUpDate = OrderEntity.PickUpDate,
                //    TotalPrice = totalPrice
                //};
                //var userEmail = user.Email;

                //if (!string.IsNullOrEmpty(userEmail))
                //{
                //    var emailSent = await Utils.SendEmail.SendOrderEmail(orderEmailDTO, userEmail);
                //}
                OrderEntity.UserId = userId;
                OrderEntity.OrderStatus = OrderEnum.PENDING;
                OrderEntity.OrderDate = DateTime.Now;
                OrderEntity.WashStatus = WashEnum.PENDING;
                OrderEntity.TotalPrice = 0;

                await _orderRepo.AddAsync(OrderEntity);
                // Prepare success response
                serviceResponse.Data = order;
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
    }
}
