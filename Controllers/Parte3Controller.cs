using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.OrderService;
using ProvaPub.Repository;
using ProvaPub.Services;
using System.Globalization;
using System.Reflection;

namespace ProvaPub.Controllers
{
	
	/// <summary>
	/// Esse teste simula um pagamento de uma compra.
	/// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
	/// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
	/// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
	{
        [HttpGet("orderCreditCard")]
        public async Task<ActionResult<Order>> PlaceOrderCreditCard(decimal paymentValue, int customerId)
        {
            return await new CreditCardOrder().PayOrder(paymentValue, customerId);
        }

        [HttpGet("orderPix")]
		public async Task<ActionResult<Order>> PlaceOrderPix(decimal paymentValue, int customerId)
		{
            return await new PixOrder().PayOrder(paymentValue, customerId);
        }

        [HttpGet("orderPayPal")]
        public async Task<ActionResult<Order>> PlaceOrderPayPal(decimal paymentValue, int customerId)
        {
            return await new PayPalOrder().PayOrder(paymentValue, customerId);
        }
    }
}
