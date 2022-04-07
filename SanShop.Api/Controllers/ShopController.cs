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
        public IActionResult GetCategories()
        {
            var categories = _service.GetCategories();
            if (categories != null && categories.Any())
                return Ok(categories);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("deliveries")]
        public IActionResult GetDeliveryOptions()
        {
            var options = _service.GetDeliveryOptions();
            if (options != null && options.Any())
                return Ok(options);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts()
        {
            var products = _service.GetProducts();
            if (products != null && products.Any())
                return Ok(products);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("productsbycategory")]
        public IActionResult GetProductsByCategory([FromQuery] string categoryId)
        {
            var products = _service.GetProductsByCategory(categoryId);
            if (products != null && products.Any())
                return Ok(products);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("product/{id}")]
        public IActionResult GetProductById(string id)
        {
            var product = _service.GetProductById(id);
            if (product != null)
                return Ok(product);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("productbyid")]
        public IActionResult GetProductByIdVer2([FromQuery] string id)
        {
            var product = _service.GetProductById(id);
            if (product != null)
                return Ok(product);
            else
                return NoContent();
        }


        [HttpPost]
        [Route("product")]
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
                DeliveryOptionsIds = model.DeliveryOptionsIds
            };
        }

    }
}
