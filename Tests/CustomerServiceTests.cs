using Microsoft.EntityFrameworkCore;
using Moq;
using ProvaPub.Controllers;
using ProvaPub.Models;
using ProvaPub.Repository;
using Moq.EntityFrameworkCore;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task Test_CanPurchaseCustomersAsync()
        {
            var customerContextMock = new Mock<TestDbContext>();

            customerContextMock.Setup<DbSet<Customer>>(x => x.Customers)
                .ReturnsDbSet(TestDbContext.getCustomerSeed());
            CustomerService customerService = new CustomerService(customerContextMock.Object);

            var controller = new Parte4Controller(customerService);

            //it should be false there is already another item for this month
            Assert.True(!(await controller.CanPurchase(5, 89.5m)));

            //must be false, first purchase must have value less than 100, this customerID has no purchase
            Assert.True(!(await controller.CanPurchase(15, 170.8m)));

            //must come true this CustomerID already contains an item so it can be greater than 100
            Assert.True(await controller.CanPurchase(10, 418));

            //you can buy less than 100 and there is no other purchase in the month
            Assert.True(await controller.CanPurchase(20, 20));
        }
    }
}
