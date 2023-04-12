using ProvaPub.Models;

namespace ProvaPub.OrderService
{
	public abstract class OrderService
	{
		public abstract Task<Order> PayOrder(decimal paymentValue, int customerId);
	}
}
