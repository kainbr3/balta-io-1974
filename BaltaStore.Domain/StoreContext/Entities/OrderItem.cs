using BaltaStore.Shared.Entities;
using FluentValidator;
using System;
using System.Collections.Generic;

namespace BaltaStore.Domain.StoreContext.Entities {
    public class OrderItem : Entity {
        public OrderItem(Product product,
            decimal quantity) {
            Product = product;
            Quantity = quantity;
            Price = product.Price;

            if(product.QuantityOnHand < quantity) {
                AddNotification("Quantity", "Produto fora de estoque");
            }
        }
        public Order Order { get; private set; }
        public Product Product { get; private set; }
        public decimal Quantity { get; private set; }    
        public decimal Price { get; private set; }
    }
}