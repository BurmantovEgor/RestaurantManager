namespace KitchenService.Data
{
    public class OrderDetailEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductCount { get; set; }
    }
}
