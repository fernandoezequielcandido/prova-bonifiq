using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Interfaces;

namespace ProvaPub.Services
{
    public class CustomerService: ICustomerService
    {
        TestDbContext _ctx;

        public CustomerService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public CustomerList ListCustomers(int page)
        {
            if (page >= 1)
            {
                var returnValue = _ctx.Customers.AsNoTracking().Skip((page - 1) * 10).Take(10).ToList();
                var next = _ctx.Customers.AsNoTracking().Skip(page * 10).Take(10).ToList();
                return new CustomerList() { HasNext = (next.Count > 0), TotalCount = returnValue.Count, Customers = returnValue };
            }
            else
                return new CustomerList() { HasNext = _ctx.Customers.Count() > 0, TotalCount = 0, Customers = new List<Customer>() };
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _ctx.Customers.FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow;
            var ordersInThisMonth = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any(w => w.OrderDate.Month == baseDate.Month));
            if (ordersInThisMonth >= 1)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            return true;
        }

    }
}
