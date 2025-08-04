using InternetShopApi.Domain.Entities;

namespace InternetShopApi.Data.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync (int id);
        Task<Order> CreateAsync (Order order);
        Task<bool> UpdateAsync (Order order);
        Task<bool> DeleteAsync (int id);
    }
}
