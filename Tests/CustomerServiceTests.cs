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
        private Mock<TestDbContext> customerContextMock;
        private Parte4Controller controller4;

        public CustomerServiceTests()
        {
            customerContextMock = new Mock<TestDbContext>();
            customerContextMock.Setup<DbSet<Customer>>(x => x.Customers)
                .ReturnsDbSet(TestDbContext.getCustomerSeed());
            CustomerService customerService = new CustomerService(customerContextMock.Object);
            controller4 = new Parte4Controller(customerService);
        }

        [Fact]
        public async Task CanPurchase_AlreadyExistInMonth()
        {
            //it should be false there is already another item for this month
            Assert.False(await controller4.CanPurchase(5, 89.5m));
        }

        [Fact]
        public async Task CanPurchase_FirstValueMustHaveLessThan100()
        {
            //must be false, first purchase must have value less than 100, this customerID has no purchase
            Assert.False(await controller4.CanPurchase(15, 170.8m));
        }

        [Fact]
        public async Task CanPurchase_AlreadyContainsValueCanBeGreaterThan100()
        {
            //must come true this CustomerID already contains an item so it can be greater than 100
            Assert.True(await controller4.CanPurchase(10, 418));
        }

        [Fact]
        public async Task CanPurchase_CanBuyLessThan100AndNoOtherInTheMonth()
        {
            //you can buy less than 100 and there is no other purchase in the month
            Assert.True(await controller4.CanPurchase(20, 20));
        }

        [Fact]
        public void CanPurchase_CustomerIdThatDoesNotExist()
        {
            //invalid customerId
            Assert.Throws<System.InvalidOperationException>(() => controller4.CanPurchase(205, 20).GetAwaiter().GetResult());
        }

        [Fact]
        public void CanPurchase_InvalidCustomerId()
        {
            //invalid customerId
            Assert.Throws<System.ArgumentOutOfRangeException>(() => controller4.CanPurchase(-40, 20).GetAwaiter().GetResult());
        }

        [Fact]
        public void CanPurchase_InvalidPurchaseValue()
        {
            //invalid value
            Assert.Throws<System.ArgumentOutOfRangeException>(() => controller4.CanPurchase(10, -20).GetAwaiter().GetResult());
        }

    }
}
