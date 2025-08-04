using InternetShopApi.Contracts.Dtos.CustomerDto;
using InternetShopApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopApi.Service.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResultDto>> GetAllCusmoresAsync();
        Task<CustomerResultDto?> GetByIdAsync(int id);
        Task<CustomerResultDto?> CreateCustomerAsync(CustomerCreateDto dto);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
