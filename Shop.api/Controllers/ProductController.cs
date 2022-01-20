using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.constant;
using Shop.entities;
using Shop.services;

namespace Shop.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductController : GeneralController<Product, IProductService>
    {
        private Response response;
        private IProductService _productService;
        public ProductController(IProductService productService) : base(productService)
        {

            response = new Response();
            _productService = productService;
        }
        [HttpPost("add")]
        public Response Create([FromBody] Product product)
        {
            var data = _productService.Add(product);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        [HttpGet("getByBrand")]
        public Response GetByBrand(int brandId)
        {
            var data = _productService.GetByBrandId(brandId);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        [HttpGet("search-by-name")]
        public Response SearchByName(int brandId, string? name)
        {
            var data = _productService.GetProductByName(brandId, name);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }
    }
}
