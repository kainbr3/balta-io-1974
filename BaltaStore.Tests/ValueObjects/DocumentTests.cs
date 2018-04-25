using BaltaStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests {
    [TestClass]
    public class DocumentTests {
        [TestMethod]
        public void ShouldReturnNotificationWhenDocumentIsNotValid() {
            var doc = new Document("73120162511");
            Assert.AreEqual(false, doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnNotNotificationWhenDocumentIsNotValid() {
            var doc = new Document("73120162515");
            Assert.AreEqual(true, doc.IsValid);
        }
    }
}