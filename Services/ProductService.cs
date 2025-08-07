using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService : IProductService
    {
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public ProductList  ListProducts(int page)
		{
            var pageSize = 10;
            var totalCount = _ctx.Products.Count();
            var products = _ctx.Products
                .OrderBy(p => p.Id) // Certifique-se de ordenar os resultados
                .Skip((page - 1) * 5)
                .Take(pageSize)
                .ToList();

            return new ProductList
            {
                HasNext = page * pageSize < totalCount,
                TotalCount = totalCount,
                Products = products
            };
		}

	}
}
