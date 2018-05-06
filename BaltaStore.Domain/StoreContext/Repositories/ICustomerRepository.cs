using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Queries;

namespace BaltaStore.Domain.StoreContext.Repositories {
    public interface ICustomerRepository {
        bool CheckDocument(string document);
        bool CheckEmail(string emailAddress);
        void Save(Customer customer);
        CustomerOrdersCountResult GetCustomerOrdersCount(); 
    }
}