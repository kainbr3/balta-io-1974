using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests {
    [TestClass]
    public class OrderTest {
        private Customer _customer;
        private Order _order;
        private Product _p1;
        private Product _p2;
        private Product _p3;
        public OrderTest() {
            var name = new Name("Alex", "Canario");
            var doc = new Document("73120162515");
            var email = new Email("alexcanario@gmail.com");
            _customer = new Customer(name, doc, email, "71 9.9183-2956");
            _order = new Order(_customer);
            _p1 = new Product("Mouse", "Mouse gamming", "mouse.png", 15M, 10);
            _p2 = new Product("Teclado", "Teclado Slim", "tecladoSlim.png", 15M, 10);
            _p3 = new Product("Fonte", "Fonte Turbo", "fonte.png", 44M, 10);
        }

        [TestMethod]
        public void ShouldCreateOrderWhenValid() {
            //Consigo criar um novo pedido

            Assert.AreEqual(true, _order.IsValid);
        }
            
        [TestMethod]
        public void StatusShouldBeCreatedWhenOrderCreated() {
            //O status de um pedido novo é created

            Assert.AreEqual(EOrderStatus.Created, _order.Status);
        }

        [TestMethod]
        public void ShouldReturnTwoWhenAddesTwoValidItems() {
            //Ao adicionar um novo item, a quantidade no pedido deve mudar
            _order.AddItem(_p1, 2);
            _order.AddItem(_p2, 2);
            
            Assert.AreEqual(2, _order.Items.Count);
        }

        [TestMethod]
        public void ShouldReturnFiveWhenAddesPurchasedFiveItems() {
            //Ao adicionar um novo item, essa quantidade deve ser subtraida da quantidade do estoque

            _order.AddItem(_p3, 5);
            Assert.AreEqual(5, _p3.QuantityOnHand);
        }

        [TestMethod] 
        public void ShouldReturnANumberWhenOrderPlaced() {
            //Ao confirmar um pedido, deve-se confirmar um número

            _order.Place();

            Assert.AreNotEqual(string.Empty, _order.Number);
        }

        [TestMethod]
        public void ShouldReturnStatusPaidWhenOrderPaid() {
            //Ao pagar um pedido, o status deve ser Payed

            _order.Pay();
            Assert.AreEqual(EOrderStatus.Paid, _order.Status);
        }
            
        [TestMethod]
        public void ShoudlReturnMoremTwoDeliveriesWhenMoreTenProducts() {
            //Deve retornar três entregas quando houverem mais de 10 produtos
            
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.Ship();

            Assert.AreEqual(3, _order.Deliveries.Count);
        }

        [TestMethod]
        public void ShouldReturnItemStatusCanceledWhenOrderCanceled() {
            //Ao cancelar um pedido, o status dos itens deve ser canceled

            _order.Cancel();
            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);
        }

        [TestMethod]
        public void ShouldCancelShippingWhenOrderCanceled() {
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.AddItem(_p1, 1);
            _order.Ship();
            _order.Cancel();

            foreach(var x in _order.Deliveries) {
                Assert.AreEqual(EDeliveryStatus.Canceled, x.Status);
            }
        }
    }
}