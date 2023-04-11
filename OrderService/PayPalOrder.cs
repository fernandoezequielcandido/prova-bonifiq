﻿using ProvaPub.Models;

namespace ProvaPub.OrderService
{
    public class PayPalOrder: OrderService
    {
        public override async Task<Order> PayOrder(decimal paymentValue, int customerId)
        {
            return await Task.FromResult(new Order()
            {
                Value = paymentValue
            });
        }
    }
}
