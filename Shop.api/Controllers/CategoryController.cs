using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.constant;
using Shop.entities;
using Shop.services;

namespace Shop.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : GeneralController<Category, ICategoryService>
    {
        private ICategoryService _categoryService;
        private Response response;
        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
            _categoryService = categoryService;
            response = new Response();
        }

        [HttpGet("get-all")]
        public Response GetAll()
        {
            var blogDetail = this._categoryService.GetAll().Where(x => !x.IsDisabled);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = blogDetail;
            response.Message = "Success";
            return response;
        }

        [HttpGet("search-by-name")]
        public Response GetByName(string name)
        {
            var blogDetail = this._categoryService.GetAll().Where(x => x.Name.Contains(name)).ToList();
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = blogDetail;
            response.Message = "Success";
            return response;
        }

    }
}
