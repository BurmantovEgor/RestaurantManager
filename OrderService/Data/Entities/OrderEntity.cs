namespace OrderService.Data.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid StatusId { get; set; }

        public StatusEntity Status { get; set; } 
        public ICollection<OrderDetailEntity> OrderDetails { get; set; } 
    }
}
