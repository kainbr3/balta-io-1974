using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Queries;
using BaltaStore.Domain.StoreContext.Repositories;

namespace BaltaStore.Tests {
    public class FakeCustomerRepository : ICustomerRepository
    {
        public bool CheckDocument(string document)
        {
            return false;
        }

        public bool CheckEmail(string emailAddress)
        {
            return false;
        }

        public CustomerOrdersCountResult GetCustomerOrdersCount()
        {
            throw new System.NotImplementedException();
        }

        public void Save(Customer customer)
        {
            //Save
        }
    }
}