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
                UserName = customer.UserName,
                Name = customer.Name,
                SurName = customer.SurName,
                Email = customer.Email

            }).ToList();

            return result;
        }

        public async Task<CustomerResultDto?> GetByIdAsync(string id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            Guard.AgainsNull(customer, nameof(customer));

            return new CustomerResultDto
            {
                CustomerId = customer.CustomerId,
                UserName = customer.UserName,
                Name = customer.Name,
                SurName = customer.SurName,
                Email = customer.Email
            };
        }

        public async Task<CustomerResultDto?> CreateCustomerAsync(Customer customer)
        {
            Guard.AgainsNull(customer, nameof(customer));
            Guard.AgainstEmpty(customer.Name, nameof(customer.Name));

            var customerCreate = await _customerRepository.CreateAsync(customer);

            return new CustomerResultDto
            {
                CustomerId = customer.CustomerId,
                UserName = customer.UserName,
                Name = customer.Name,
                SurName = customer.SurName,
                Email = customer.Email
            };


        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var customer = _customerRepository.GetByIdAsync(id);
            Guard.AgainsNull(customer, nameof(customer));

            return await _customerRepository.DeleteAsync(id);
        }

        public async Task<CustomerResultDto> UpdateCustomerAsync(string id, CustomerUpdateDto dto)
        {
            Guard.AgainsNull(dto, nameof(dto));
            Guard.AgainstEmpty(dto.Name, nameof(dto.Name));

            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
                throw new ArgumentException($"Customer with Id {id} not found");
            existingCustomer.Name = dto.Name;

            await _customerRepository.UpdateAsync(existingCustomer);

            return new CustomerResultDto
            {
                CustomerId = existingCustomer.CustomerId,
                Name = existingCustomer.Name,
                SurName = existingCustomer.SurName,
                Email = existingCustomer.Email
            };
        }
    }
}
