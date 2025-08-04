using InternetShopApi.Contracts.Dtos.OrderDto;
using InternetShopApi.Domain.Entities;
using InternetShopApi.Dtos.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopApi.Service.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResultDto>> GetAllOrderAsync();
        Task<OrderResultDto?> GetByIdAsync(int id);
        Task<OrderResultDto> CreateOrderAsync(OrderCreateDto dto);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
