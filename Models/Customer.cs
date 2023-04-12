using System.Collections.ObjectModel;

namespace ProvaPub.Models
{
	public class Customer
	{
        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
            Orders = new Collection<Order>();    
        }

        public int Id { get; private set; }
		public string Name { get; private set; }
		public ICollection<Order> Orders { get; set; }
	}
}
