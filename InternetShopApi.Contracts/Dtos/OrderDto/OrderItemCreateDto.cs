﻿namespace InternetShopApi.Contracts.Dtos.OrderDto
{
    public class OrderItemCreateDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
