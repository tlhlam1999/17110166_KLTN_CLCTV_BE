
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
        private ICartService _cartService;
        private Response response;
        public HomeController(ICategoryService categoryService, IOrderService orderService,
            IProductService productService, IBrandService brandService, IBlogService blogService, ICommentService commentService,
            IUserService userService, IOrderDetailService orderDetailService, ICompositionService compositionService,
            ICartService cartService)
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
            _cartService = cartService;
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
            product.CompositionName = _compositionService.Get(product.CompositionId).Name;
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

        [HttpPost("get-cart")]
        public Response GetCart(int? userId, string clientIp)
        {
            var carts = _cartService.GetCart(userId, clientIp);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = carts;
            response.Message = "Success";
            return response;
        }

        [HttpPost("add-to-cart")]
        public Response AddToCart(Cart cart)
        {
            var data = _cartService.Add(cart);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        [HttpGet("delete-cart")]
        public Response DeleteCart(int id)
        {
            var data = _cartService.Delete(id);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }


        [HttpGet("get-blog")]
        public Response GetBlog(string? title)
        {
            var blogs = new List<Blog>();
            if (string.IsNullOrEmpty(title))
            {
                blogs = this._blogService.GetAll();
            }
            else
            {
                blogs = this._blogService.GetAll().Where(x => x.Title.Contains(title)).ToList();
            }
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

        [HttpGet("search")]
        public Response Search(string name)
        {
            var data = this._compositionService.GetByName(name);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        [HttpGet("get-order-by-user")]
        public Response GetOrderByUser(int userId)
        {
            var orders = this._orderService.GetAll().Where(x => x.UserId == userId).ToList();
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = orders;
            response.Message = "Success";
            return response;
        }
        [HttpGet("get-order-detail")]
        public Response GetOrderDetail(int orderId)
        {
            var orders = this._orderDetailService.GetAll().Where(x => x.OrderId == orderId).ToList();
            foreach (var order in orders)
            {
                order.Product = _productService.Get(order.ProductId);
            }
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = orders;
            response.Message = "Success";
            return response;
        }

    }
}
