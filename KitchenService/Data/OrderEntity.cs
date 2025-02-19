namespace KitchenService.Data
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public string StatusId { get; set; }
        public StatusEntity Status { get; set; }
        public ICollection<OrderDetailEntity> OrderDetails { get; set; }
    }
}
