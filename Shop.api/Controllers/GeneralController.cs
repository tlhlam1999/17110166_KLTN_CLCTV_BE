using Microsoft.AspNetCore.Mvc;
using Shop.constant;
using Shop.services;

namespace Shop.api.Controllers
{
    public abstract class GeneralController<TEntity, TService> : Controller
           where TEntity : class
           where TService : IGeneralService<TEntity>
    {
        private TService service;
        private Response response;

        public GeneralController(TService service)
        { 
            this.service = service;
            response = new Response();
        }
        [HttpGet("get")]
        public Response GetAll()
        {
            var data = service.GetAll();
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }
        [HttpPost("create")]
        public Response Create(TEntity t)
        {
            var data = service.Add(t);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        [HttpPost("edit")]
        public Response Edit(TEntity t)
        {
            var data = service.Update(t);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }
        [HttpGet("delete")]
        public Response Delete(int id)
        {
            var isDel = service.Delete(id);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = isDel;
            response.Message = "Success";
            return response;
        }

    }
}
