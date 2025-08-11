using InternetShopApi.Domain.Entities;

namespace InternetShopApi.Data.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync (int id);
        Task<OrderItem?> GetByIdItemAsync (int id);
        Task<Order> CreateAsync (Order order);
        Task<bool> UpdateAsync (OrderItem order);
        Task<bool> DeleteAsync (int id);
    }
}
