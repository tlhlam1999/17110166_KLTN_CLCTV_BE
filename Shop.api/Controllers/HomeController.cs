
using Microsoft.AspNetCore.Mvc;
using Shop.constant;
using Shop.entities;
using Shop.services;

namespace Shop.api.Controllers
{
    [ApiController]
    [Route("api/homes")]
    public class HomeController : Controller
    {
        private ICategoryService _categoryService;
        private IProductService _productService;
        private IBrandService _brandService;
        private IOrderService _orderService;
        private IBlogService _blogService;
        private ICommentService _commentService;
        private IUserService _userService;
        private IOrderDetailService _orderDetailService;
        private ICompositionService _compositionService;
        private Response response;
        public HomeController(ICategoryService categoryService, IOrderService orderService,
            IProductService productService, IBrandService brandService, IBlogService blogService, ICommentService commentService,
            IUserService userService, IOrderDetailService orderDetailService, ICompositionService compositionService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _productService = productService;
            _brandService = brandService;
            _orderService = orderService;
            _commentService = commentService;
            _userService = userService;
            _orderDetailService = orderDetailService;
            _compositionService = compositionService;
            response = new Response();
        }


        [HttpPost("products")]
        public Response GetProduct(int brandId, string? search)
        { 
            var data = _productService.GetProduct(brandId, search);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        [HttpGet("products")]
        public Response GetProduct(int id)
        {
            Product product = _productService.Get(id);
            product.Composition = _compositionService.Get(product.CompositionId).Name;
            product.BrandName = _brandService.Get(product.BrandId).Name;
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = product;
            response.Message = "Success";
            return response;
        }

        [HttpGet("categories")]
        public Response GetCategory()
        {
            var data = _categoryService.GetAll();

            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }
        [HttpGet("brands")]
        public Response GetBrand(int categoryId)
        {
            var datas = _brandService.GetByCategoryId(categoryId);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = datas;
            response.Message = "Success";
            return response;
        }

        [HttpPost("create-order")]
        public Response CreateOrder(Order order)
        {
            var data = _orderService.CreateOrder(order);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        [HttpPost("get-order-detail")]
        public Response GetOrderDetail(int? userId, string clientIp)
        {
            var orderDetails = _orderDetailService.GetOrderDetail(userId, clientIp);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = orderDetails;
            response.Message = "Success";
            return response;
        }

        [HttpPost("create-order-detail")]
        public Response CreateOrder(OrderDetail orderDetail)
        {
            orderDetail.Status = 1;
            orderDetail.DateTrade = DateTime.Now.ToString();
            var data = _orderDetailService.CreateOrderDetail(orderDetail);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        [HttpGet("delete-order-detail")]
        public Response DeleteOrder(int id)
        {
            var data = _orderDetailService.Delete(id);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }


        [HttpGet("getBlog")]
        public Response GetBlog()
        {
            var blogs = this._blogService.GetAll();
            foreach (Blog blog in blogs)
            {
                var user = _userService.Get(blog.UserId);
                blog.Author = user.UserName;
            }
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = blogs;
            response.Message = "Success";
            return response;
        }
        [HttpGet("getBlogById")]
        public Response GetById(int blogId)
        {
            var blogDetail = this._blogService.Get(blogId);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = blogDetail;
            response.Message = "Success";
            return response;
        }

        [HttpGet("getByBlogId")]
        public Response GetCommentByBlogId(int blogId)
        {
            var comments = this._commentService.GetByBlogId(blogId);
            foreach (var comment in comments)
            {
                comment.UserName = _userService.Get((int)comment.UserId).UserName;
                comment.DateCreated = DateTime.Now.ToString();
            }
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = comments;
            response.Message = "Success";
            return response;
        }

        [HttpPost("createComment")]
        public Response CreateComment([FromBody] Comment comment)
        {
            var user = _userService.Get((int)comment.UserId);
            comment.UserName = user.UserName;
            comment.DateCreated = DateTime.Now.ToString();
            var data = this._commentService.Add(comment);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }


        [HttpPost("create-customer")]
        public Response CreateCustomer([FromBody] User user)
        {
            user.Role = 2;
            var data = this._userService.CreateCustomer(user);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

    }
}
