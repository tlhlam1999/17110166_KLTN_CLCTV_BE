using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.constant;
using Shop.entities;
using Shop.services;

namespace Shop.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/brands")]
    public class BrandController : GeneralController<Brand, IBrandService>
    {
        private IBrandService _brandService;
        private Response response;
        public BrandController(IBrandService brandService) : base(brandService)
        {
            _brandService = brandService;
            response = new Response();
        }

        [HttpGet("getByCategoryId")]
        public Response GetByCategoryId(int categoryId)
        {
            var brands = _brandService.GetByCategoryId(categoryId); 
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = brands;
            response.Message = "Success";
            return response; 
        }

        [HttpGet("search-by-name")]
        public Response SearchByName(int categoryId, string name)
        {
            var brands = _brandService.GetByCategoryId(categoryId).Where(x=>x.Name.Contains(name)).ToList();
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = brands;
            response.Message = "Success";
            return response;
        }
    }
}
