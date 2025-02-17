namespace KitchenService.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<string> Items { get; set; } = new();

        public override string ToString()
        {
            return $"id = {this.Id} items = {this.Items} ";
        }
    }
}
