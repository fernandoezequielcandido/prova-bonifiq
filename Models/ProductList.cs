namespace ProvaPub.Models
{
	public class ProductList: BaseList
	{
        public ProductList()
        {
            Products = new List<Product>();
        }
        public List<Product> Products { get; set; }
	}
}
