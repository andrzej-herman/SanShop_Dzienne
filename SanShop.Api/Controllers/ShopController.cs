using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanShop.Api.Services;
using SanShop.Common.Entities;
using SanShop.Common.Models;

namespace SanShop.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ShopController : ControllerBase
    {
        private readonly IProductService _service;

        public ShopController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("categories")]
        public IEnumerable<Category> GetCategories()
        {
            return _service.GetCategories();
        }

        [HttpGet]
        [Route("deliveries")]
        public IEnumerable<DeliveryOption> GetDeliveryOptions()
        {
            return _service.GetDeliveryOptions();
        }

        [HttpGet]
        [Route("products")]
        public IEnumerable<Product> GetProducts()
        {
            return _service.GetProducts();
        }

        [HttpGet]
        [Route("productsbycategory")]
        public IEnumerable<Product> GetProductsByCategory([FromQuery] string categoryId)
        {
            return _service.GetProductsByCategory(categoryId);
        }

        [HttpGet]
        [Route("product/{id}")]
        public Product GetProductById(string id)
        {
            return _service.GetProductById(id);
        }

        [HttpGet]
        [Route("productbyid")]
        public Product GetProductByIdVer2([FromQuery] string id)
        {
            return _service.GetProductById(id);
        }


        [HttpPost]
        [Route("product")]
        [Authorize]
        public IActionResult AddProduct(AddProductModel model)
        {
            var product = CreateProduct(model);
            var result = _service.AddProduct(product);
            if (result != null)
                return Ok(result);
            else
                return BadRequest("Błąd podczas dodawania produktu");
        }

        private Product CreateProduct(AddProductModel model)
        {
            return new Product
            {
                Id = Helper.GetId(),
                SellerId = model.SellerId,
                CategoryId = model.CategoryId,
                Name = model.Name,
                Quantity = model.Quantity,
                Description = model.Description,
                Price = model.Price,
                DateAdd = DateTime.Now,
                ImageUrl = model.ImageUrl,
                IsPromoted = model.IsPromoted,
                DeliveryOptions = _service.GetDeliveryOptions().ToList()
        };
        }

    }
}
