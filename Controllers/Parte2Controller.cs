using Microsoft.AspNetCore.Mvc;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
        /// <summary>
        /// Precisamos fazer algumas alterações:
        /// 1 - Não importa qual page é informada, sempre são retornados os mesmos resultados. Faça a correção.
        /// 2 - Altere os códigos abaixo para evitar o uso de "new", como em "new ProductService()". Utilize a Injeção de Dependência para resolver esse problema
        /// 3 - Dê uma olhada nos arquivos /Models/CustomerList e /Models/ProductList. Veja que há uma estrutura que se repete. 
        /// Como você faria pra criar uma estrutura melhor, com menos repetição de código? E quanto ao CustomerService/ProductService. Você acha que seria possível evitar a repetição de código?
        /// OBS:
		/// Mudanças feitas faltou apenas onde não parece ser possível fazer algo mais generico apesar de serem parecidos CustomerService/ProductService os metodos que listam os itens com pagina
		/// usando dynamics ou mesmo generics esbarraria na parte onde as listas são montadas _ctx.Customer ou _ctx.Products, precisamos de itens diferentes um para cada tipo objeto  
		/// ainda que tenha logica igual em alguns pontos
        /// </summary>
        IProductService _productService;
		ICustomerService _customerService;
		public Parte2Controller(IProductService productService, ICustomerService customerService)
		{
			_productService = productService;
			_customerService = customerService;
		}
	
		[HttpGet("products")]
        public ProductList ListProducts(int page)
		{
			return _productService.ListProducts(page);
		}

		[HttpGet("customers")]
		public CustomerList ListCustomers(int page)
		{
			return _customerService.ListCustomers(page);
		}
	}
}
