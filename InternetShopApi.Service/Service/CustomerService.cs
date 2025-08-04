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
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

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
            if(dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Customer name can't by empty");

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
            if(customer == null)
                throw new ArgumentNullException(nameof(customer));

            return await _customerRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            var existingCustomer = _customerRepository.UpdateAsync(customer);
            if (existingCustomer == null)
                throw new ArgumentException("Customer not found");

            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Customer name can't be empty");

            return await existingCustomer;
        }
    }
}
