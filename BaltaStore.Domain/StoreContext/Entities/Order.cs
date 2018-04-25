using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Shared.Entities;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaltaStore.Domain.StoreContext.Entities {
    public class Order : Entity {
        private readonly IList<OrderItem> _items;
        private readonly IList<Delivery> _deliveries;
        public Order(Customer customer) {
            Customer = customer;
            CreateDate = DateTime.Now;
            Status = EOrderStatus.Created;
            _items = new List<OrderItem>();
            _deliveries = new List<Delivery>();
        }
        
        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public EOrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
        public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

        public void AddItem(Product product, decimal quantity) {
            if(quantity > product.QuantityOnHand) {
                AddNotification("OrderItem", $"Produto {product.Title} não tem {quantity} item(ns) em estoque");
            }
            var item = new OrderItem(product, quantity);
            _items.Add(item);
            product.DecreaseQuantity(quantity);
        }

        //public void AddDelivery(Delivery delivery) {
        //    _deliveries.Add(delivery);
        //}

        //To Place An Order
        //Criar um pedido
        public void Place() {
            //Gera o n�mero do pedido
            Number = Guid.NewGuid().ToString().Replace("- ", "").Substring(0, 8).ToUpper();
            //Validar
            if(_items.Count == 0) {
                AddNotification("Order", "Esse pedido n�o possui itens");
            }
        }

        //Pagar um pedido
        public void Pay() {
            Status = EOrderStatus.Paid;
            var delivery = new Delivery(DateTime.Now.AddDays(5));
            delivery.Ship();
            _deliveries.Add(delivery);
        }

        //Enviar um pedido
        public void Ship() {
            //Dividir a entrega a cada cinco produtos
            var deliveries = new List<Delivery>();
            deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
            var count = 1;
            //Quebra as entregas de 5 em 5
            foreach (var item in _items) {
                if(count == 5) {
                    count = 0;
                    deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
                }
                count++;
            }
            //Envia as entregas
            deliveries.ForEach(x => x.Ship());
            //Adiciona as entregas ao pedido
            deliveries.ForEach(x => _deliveries.Add(x));
        }
        
        //Cancelar um pedido
        public void Cancel() {
            Status = EOrderStatus.Canceled;
            _deliveries.ToList().ForEach(x => x.Cancel());
        }
    }
}