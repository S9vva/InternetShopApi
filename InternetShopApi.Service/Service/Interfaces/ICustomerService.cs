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
        Task<CustomerResultDto?> GetByIdAsync(string id);
        Task<CustomerResultDto?> CreateCustomerAsync(Customer customer);
        Task<CustomerResultDto> UpdateCustomerAsync(string id, CustomerUpdateDto dto);
        Task<bool> DeleteCustomerAsync(string id);
    }
}
