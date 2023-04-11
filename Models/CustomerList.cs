namespace ProvaPub.Models
{
	public class CustomerList: BaseList
	{
        public CustomerList()
        {
            Customers = new List<Customer>();
        }
        public List<Customer> Customers { get; set; }
	}
}
