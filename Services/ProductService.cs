using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace ProvaPub.Services
{
	public class ProductService: IProductService
	{
		TestDbContext _ctx;

        public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public ProductList  ListProducts(int page)
		{
			if (page >= 1)
			{
				var returnValue = _ctx.Products.AsNoTracking().Skip((page -1) * 10).Take(10).ToList();
				var next = _ctx.Products.AsNoTracking().Skip(page * 10).Take(10).ToList();
				return new ProductList() { HasNext = (next.Count > 0), TotalCount = returnValue.Count, Products = returnValue };
            }
			else
                return new ProductList() { HasNext = _ctx.Products.Count() > 0, TotalCount = 0, Products = new List<Product>() };
        }

	}
}
