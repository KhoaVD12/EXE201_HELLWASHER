using DataAccess.Enum;

namespace BusinessObject.ViewModels.Order
{
    public class OrderResponse
    {
        public int? orderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CusomterPhone { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public OrderEnum OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? PickUpDate { get; set; }
        public string? ConfirmImage { get; set; }

    }
}
