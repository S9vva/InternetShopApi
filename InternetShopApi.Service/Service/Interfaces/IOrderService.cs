using InternetShopApi.Contracts.Dtos.OrderDto;
using InternetShopApi.Dtos.OrderDto;


namespace InternetShopApi.Service.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResultDto>> GetAllOrderAsync();
        Task<OrderResultDto?> GetByIdAsync(int id);
        Task<OrderResultDto> CreateOrderAsync(OrderCreateDto dto);
        Task<OrderItemResultDto> UpdateOrderAsync(int id, OrderItemCreateDto dto);
        Task<bool> DeleteOrderAsync(int id);
    }
}
