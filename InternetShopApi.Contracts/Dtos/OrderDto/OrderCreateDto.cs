namespace InternetShopApi.Contracts.Dtos.OrderDto
{
    public class OrderCreateDto
    {
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItemCreateDto> Items { get; set; }
    }
}
