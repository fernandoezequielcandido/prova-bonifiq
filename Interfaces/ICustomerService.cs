using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface ICustomerService
    {
        public CustomerList ListCustomers(int page);

        public Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
