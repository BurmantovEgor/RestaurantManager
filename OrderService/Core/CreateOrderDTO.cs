using OrderService.Data.Entities;

namespace OrderService.Core
{
    public class CreateOrderDTO
    {
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<CreateOrderDetailsDTO> OrderDetails { get; set; }
    }
}
