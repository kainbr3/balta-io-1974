using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests {
    [TestClass]
    public class CreateCustomerCommandTests {
        [TestMethod]
        public void ShouldValidateWhenCommandIsValid() {
            var cmd = new CreateCustomerCommand();
            cmd.FirstName = "Alex";
            cmd.LastName = "Canario";
            cmd.Document = "73120162515";
            cmd.Email = "alexcanario@gmail.com";
            cmd.Phone = "71 9.9183-2956";

            Assert.AreEqual(true, cmd.Validated());
        }
    }
}