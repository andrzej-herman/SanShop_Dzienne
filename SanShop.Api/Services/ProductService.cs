using SanShop.Common.Entities;

namespace SanShop.Api.Services
{
    public class ProductService : IProductService
    {
        private List<Category> _categories = new List<Category>
        {
            new Category { Id = Helper.GetId(), Name = "Sport" },
            new Category { Id = Helper.GetId(), Name = "Elektronika" },
            new Category { Id = Helper.GetId(), Name = "Moda" },
            new Category { Id = Helper.GetId(), Name = "Motoryzacja" },
            new Category { Id = Helper.GetId(), Name = "Dom i ogród" },
            new Category { Id = Helper.GetId(), Name = "Uroda" },
        };

        private List<DeliveryOption> _deliveryOptions = new List<DeliveryOption>
        {
            new DeliveryOption { Id = Helper.GetId(), Name = "Poczta Polska", Price = 4.99m, DeliveryDays = 10  },
            new DeliveryOption { Id = Helper.GetId(), Name = "Paczkomat InPost", Price = 9.99m, DeliveryDays = 4  },
            new DeliveryOption { Id = Helper.GetId(), Name = "Kurier DHL", Price = 14.99m, DeliveryDays = 1  }
        };

        private List<Product> _products = new List<Product>();

        private List<Comment> _comments = new List<Comment>();

        public Product AddProduct(Product product)
        {
            _products.Add(product);

            return product;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categories.OrderBy(c => c.Name);
        }

        public IEnumerable<DeliveryOption> GetDeliveryOptions()
        {
            return _deliveryOptions.OrderBy(dopt => dopt.Price);
        }

        public Product GetProductById(string id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            product.Comments = _comments.Where(c => c.ProductId == product.Id).ToList();
            product.DeliveryOptions = _deliveryOptions.ToList();
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _products.OrderByDescending(p => p.IsPromoted);
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).OrderByDescending(p => p.IsPromoted);
        }
    }
}
