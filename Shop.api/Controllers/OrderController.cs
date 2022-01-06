using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.constant;
using Shop.entities;
using Shop.services;

namespace Shop.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/orders")]
    public class OrderController : GeneralController<Order, IOrderService>
    {
        private readonly IOrderService _orderService; 
        private Response response;
        public OrderController(IOrderService orderService) : base(orderService)
        {
            _orderService = orderService; 
            response = new Response();
        }
        [HttpGet("get-orders")]
        public Response GetAll()
        {
            var orders = this._orderService.GetAll(); 
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = orders;
            response.Message = "Success";
            return response;
        }


        [HttpGet("searchOrderByCode")]
        public Response SearchOrderByCode(string? code)
        {
            var orders = this._orderService.SearchOrderByCode(code);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = orders;
            response.Message = "Success";
            return response;
        }
        
    }
}
