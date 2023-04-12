using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProvaPub.Repository
{

	public class TestDbContext : DbContext
	{
		public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
		{
		}

        public TestDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Customer>().HasData(getCustomerSeed());
			modelBuilder.Entity<Product>().HasData(getProductSeed());
		}

		public static Customer[] getCustomerSeed()
		{
			List<Customer> result = new();
			for (int i = 0; i < 20; i++)
			{
				Customer customerInsert = new Customer((i+ 1), new Faker().Person.FullName);
				if (i == 4)
				{
					customerInsert.Orders.Add(new Order { OrderDate = DateTime.UtcNow });
				}
				if (i == 9)
				{
                    customerInsert.Orders.Add(new Order { Value = 105.87m, OrderDate = new DateTime(2022, 10, 10) });
                }

				result.Add(customerInsert);
			}
			return result.ToArray();
		}
		private Product[] getProductSeed()
		{
			List<Product> result = new();
			for (int i = 0; i < 20; i++)
			{
				result.Add(new Product()
				{
					Id = i + 1,
					Name = new Faker().Commerce.ProductName()
				});
			}
			return result.ToArray();
		}

		public virtual DbSet<Customer> Customers{ get; set; }
		public DbSet<Product> Products{ get; set; }
		public DbSet<Order> Orders { get; set; }
	}
}
