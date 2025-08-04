using InternetShopApi.Contracts.Dtos.OrderDto;

namespace InternetShopApi.Dtos.OrderDto
{
    public class OrderResultDto
    {
        public int OrderId { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItemResultDto> Items { get; set; }
    }
}
