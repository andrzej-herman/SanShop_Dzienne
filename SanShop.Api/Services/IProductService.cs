using SanShop.Common.Entities;

namespace SanShop.Api.Services
{
    public interface IProductService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<DeliveryOption> GetDeliveryOptions();
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProductsByCategory(string categoryId);
        Product GetProductById(string id);
        Product AddProduct(Product product);
    }
}
