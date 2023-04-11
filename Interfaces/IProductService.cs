using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface IProductService
    {
        public ProductList ListProducts(int page);
    }
}
