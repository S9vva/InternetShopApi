using InternetShopApi.Contracts.Dtos.OrderDto;
using InternetShopApi.Data.Repository.Interfaces;
using InternetShopApi.Domain.Entities;
using InternetShopApi.Dtos.OrderDto;
using InternetShopApi.Service.Service.Interfaces;


namespace InternetShopApi.Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public async Task<IEnumerable<OrderResultDto>> GetAllOrderAsync()
        {
            var order = await _orderRepository.GetAllAsync();

            var result = order.Select(order => new OrderResultDto
            {
                OrderId = order.OrderId,
                DateTime = order.OrderDate,
                CustomerId = order.CustomerId,
                Items = order.Items.Select(i => new OrderItemResultDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Price = i.Product?.Price ?? 0,
                    Quantity = i.Quantity
                }).ToList()

            }).ToList();

            return result;
        }

        public async Task<OrderResultDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            Guard.AgainsNull(order, nameof(order));
            return new OrderResultDto
            {
                OrderId = order.OrderId,
                DateTime = order.OrderDate,
                CustomerId = order.CustomerId,
                Items = order.Items.Select(i => new OrderItemResultDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Price = i.Product?.Price ?? 0,
                    Quantity = i.Quantity
                }).ToList(),
            };
        }
        public async Task<OrderResultDto> CreateOrderAsync(OrderCreateDto dto)
        {
            Guard.AgainsNull(dto, nameof(dto));

            var order = new Order
            {
                OrderDate = dto.OrderDate,
                CustomerId = dto.CustomerId,
                Items = dto.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            var orderCreate = await _orderRepository.CreateAsync(order);

            return new OrderResultDto
            {
                OrderId = orderCreate.OrderId,
                DateTime = orderCreate.OrderDate,
                CustomerId = orderCreate.CustomerId,
                Items = orderCreate.Items.Select(i => new OrderItemResultDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            Guard.AgainsNull(order, nameof(order));

            return await _orderRepository.DeleteAsync(id);
        }

        public async Task<OrderItemResultDto> UpdateOrderAsync(int id, OrderItemCreateDto dto)
        {
            Guard.AgainsNull(dto, nameof(dto));

           var existingOrder = await _orderRepository.GetByIdItemAsync(id);
            if (existingOrder == null)
                throw new ArgumentException($"Order with Id {id} not found");
           
           existingOrder.Quantity = dto.Quantity;

            await _orderRepository.UpdateAsync(existingOrder);

            return new OrderItemResultDto
            {
                Quantity = dto.Quantity,
                ProductId = dto.ProductId,
            };

            
        }
    }
}
