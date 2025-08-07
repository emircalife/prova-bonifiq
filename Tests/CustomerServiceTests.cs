using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        private TestDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var ctx = new TestDbContext(options);
            return ctx;
        }

        [Fact]
        public async Task Throw_When_CustomerId_IsInvalid()
        {
            var ctx = GetDbContext("Db1");
            var service = new CustomerService(ctx);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.CanPurchase(0, 50));
        }

        [Fact]
        public async Task Throw_When_PurchaseValue_IsInvalid()
        {
            var ctx = GetDbContext("Db2");
            var service = new CustomerService(ctx);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.CanPurchase(1, 0));
        }

        [Fact]
        public async Task Throw_When_Customer_Does_Not_Exist()
        {
            var ctx = GetDbContext("Db3");
            var service = new CustomerService(ctx);

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.CanPurchase(999, 50));
        }

        [Fact]
        public async Task ReturnFalse_When_Customer_Has_Purchase_This_Month()
        {
            var ctx = GetDbContext("Db4");
            ctx.Customers.Add(new Customer { Id = 1, Name = "Emir" });
            ctx.Orders.Add(new Order { CustomerId = 1, OrderDate = DateTime.UtcNow });
            await ctx.SaveChangesAsync();

            var service = new CustomerService(ctx);
            var result = await service.CanPurchase(1, 50);

            Assert.False(result);
        }

        [Fact]
        public async Task ReturnFalse_When_FirstTimeCustomer_Buys_More_Than_100()
        {
            var ctx = GetDbContext("Db5");
            ctx.Customers.Add(new Customer { Id = 2, Name = "Teofilo" });
            await ctx.SaveChangesAsync();

            var service = new CustomerService(ctx);
            var result = await service.CanPurchase(2, 150);

            Assert.False(result);
        }

        [Fact]
        public async Task ReturnTrue_When_All_Rules_Pass()
        {
            var ctx = GetDbContext("Db7");
            ctx.Customers.Add(new Customer { Id = 3, Name = "Calife" });
            await ctx.SaveChangesAsync();

            var service = new CustomerService(ctx);
            var result = await service.CanPurchase(3, 50);

            Assert.True(result);
        }
    }
}
