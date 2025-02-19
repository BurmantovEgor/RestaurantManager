namespace PaymentService.Data.Entities
{
        public class OrderEntity
        {
            public Guid Id { get; set; }
            public decimal TotalPrice { get; set; }
            public string StatusId { get; set; }
            public StatusEntity Status { get; set; }
        }

    
}
