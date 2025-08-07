using ProvaPub.Models;

namespace ProvaPub.Services
{
    public interface IProductService
    {
        ProductList ListProducts(int page);
    }

}
