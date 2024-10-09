using AutoMapper;
using BusinessObject.IService;
using BusinessObject.ViewModels.OrderDTO;
using DataAccess;
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
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        private readonly WashShopContext _dbcontext;

        public OrderService(IOrderRepo orderRepo, IMapper mapper, WashShopContext dbcontext)
        {
            _orderRepo = orderRepo ?? throw new ArgumentNullException(nameof(orderRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }
        public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetAllOrder()
        {
            var response = new ServiceResponse<IEnumerable<OrderDTO>>();

            try
            {
                var orders = await _orderRepo.GetAllOrders();


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


        /*public async Task<ServiceResponse<int>> AddOrder(OrderDTO order)
        {
            var serviceResponse = new ServiceResponse<int>();

            try
            {
                // Map DTO to entity
                var OrderEntity = _mapper.Map<Order>(order);
                //if (OrderEntity.OrderStatusId == 0 || OrderEntity.OrderStatusId == null)
                //{
                //    throw new Exception("Failed to generate order status ID.");
                //}
                //if (OrderEntity.WashStatusId == 0 || OrderEntity.WashStatusId == null)
                //{
                //    throw new Exception("Failed to generate wash status ID.");
                //}
                if (OrderEntity.CartId == 0 || OrderEntity.CartId == null)
                {
                    throw new Exception("Failed to generate cart ID.");
                }
                // Add zodiac product to repository
                // Ensure the product ID is set after adding to the repository
                var orderEmailDTO = new OrderDTO 
                {
                    UserName = OrderEntity.UserName,
                    Address = OrderEntity.Address,
                    PickUpDate = OrderEntity.PickUpDate,
                    TotalPrice = OrderEntity.TotalPrice
                };
                var userEmail = OrderEntity.UserEmail;
                if (!string.IsNullOrEmpty(userEmail))
                {
                    var emailSent = await Utils.SendEmail.SendOrderEmail(orderEmailDTO, userEmail);
                }
                await _orderRepo.AddOrder(OrderEntity);
                // Prepare success response
                serviceResponse.Data = OrderEntity.OrderId;
                serviceResponse.Success = true;
                serviceResponse.Message = "Order created successfully! Please check your email for order details.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"{ex.Message}";
            }

            return serviceResponse;
        }*/


        /*public async Task<ServiceResponse<UpdateOrderRequest>> UpdateOrder(UpdateOrderRequest orderRequest)
        {
            var response = new ServiceResponse<UpdateOrderRequest>();
            try
            {
                var order = await _orderRepo.GetOrderWithDetailsAsync(orderRequest.OrderId);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order not found.";
                    return response;
                }
                order.OrderStatus = OrderEnum.CONFIRMED;
                order.Address = orderRequest.Address;
                order.UserName = orderRequest.UserName;
                order.OrderDate = orderRequest.OrderDate;
                order.UserEmail = orderRequest.UserEmail;
                order.UserPhone = orderRequest.UserPhone;
                order.TotalPrice = orderRequest.TotalPrice;
                order.WashStatus = WashEnum.WASHING;
                order.PickUpDate = orderRequest.PickUpDate;
                await _orderRepo.Update(order);

                response.Success = true;
                response.Message = "Order updated successfully.";
                response.Data = orderRequest;
                return response;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex.Message}";
                return response;
            }
            
        }*/
    }
}
