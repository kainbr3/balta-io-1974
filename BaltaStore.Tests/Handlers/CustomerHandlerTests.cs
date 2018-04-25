using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using BaltaStore.Domain.StoreContext.Handler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests {
    [TestClass]
    public class CustomerHandlerTests {
        [TestMethod]
        public void ShouldRegisterCustomerWhenCommandIsValid() {
            var command = new CreateCustomerCommand();

            command.FirstName = "Alex";
            command.LastName = "Canario";
            command.Document = "73120162515";
            command.Email = "alexcanario@gmail.com";
            command.Phone = "71 9.9183-2956";

            Assert.AreEqual(true, command.Validated());

            var customer = new CustomerHandler(new FakeCustomerRepository(), new FakeEmailService());
            var result = customer.Handle(command);

            Assert.AreEqual(true, customer.IsValid);
            Assert.AreNotEqual(null, result);
        }
    }
}