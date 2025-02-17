namespace OrderService.Data.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public List<string> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Новый заказ";

    }
}
