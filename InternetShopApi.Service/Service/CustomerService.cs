using InternetShopApi.Contracts.Dtos.CustomerDto;
using InternetShopApi.Data.Repository.Interfaces;
using InternetShopApi.Domain.Entities;
using InternetShopApi.Service.Service.Interfaces;


namespace InternetShopApi.Service.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

        public async Task<IEnumerable<CustomerResultDto>> GetAllCusmoresAsync()
        {
            var customer = await _customerRepository.GetAllAsync();

            var result = customer.Select(customer => new CustomerResultDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                SurName = customer.SurName,
                Email = customer.Email

            }).ToList();

            return result;
        }

        public async Task<CustomerResultDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            Guard.AgainsNull(customer, nameof(customer));

            return new CustomerResultDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                SurName = customer.SurName,
                Email = customer.Email
            };
        }

        public async Task<CustomerResultDto?> CreateCustomerAsync(CustomerCreateDto dto)
        {
            Guard.AgainsNull(dto, nameof(dto));
            Guard.AgainstEmpty(dto.Name, nameof(dto.Name));

            var customer = new Customer
            {
                Name = dto.Name,
                SurName = dto.SurName,
                Email = dto.Email
            };

            var customerCreate = await _customerRepository.CreateAsync(customer);

            return new CustomerResultDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                SurName = customer.SurName,
                Email = customer.Email
            };


        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = _customerRepository.GetByIdAsync(id);
            Guard.AgainsNull(customer, nameof(customer));

            return await _customerRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            Guard.AgainsNull(customer, nameof(customer));
            Guard.AgainstEmpty(customer.Name, nameof(customer.Name));

            var existingCustomer = _customerRepository.UpdateAsync(customer);
            if (existingCustomer == null)
                throw new ArgumentException("Customer not found");

            return await existingCustomer;
        }
    }
}
