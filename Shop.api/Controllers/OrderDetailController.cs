﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.constant;
using Shop.entities;
using Shop.services;

namespace Shop.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/orderDetails")]
    public class OrderDetailController : GeneralController<OrderDetail, IOrderDetailService>
    {

        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;
        private Response response;
        public OrderDetailController(IOrderDetailService orderDetailService, IProductService productService)
            : base(orderDetailService)
        {
            _orderDetailService = orderDetailService;
            _productService = productService;
            response = new Response();
        }

        [HttpGet("get-order-details")]
        public Response GetByOrderId(int orderId)
        {
            var orderDetails = _orderDetailService.GetByOrderId(orderId);
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.Product = _productService.Get(orderDetail.ProductId);
            }
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = orderDetails;
            response.Message = "Success";
            return response;

        }
    }
}
