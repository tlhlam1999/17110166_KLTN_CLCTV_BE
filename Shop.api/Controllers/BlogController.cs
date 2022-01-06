using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.constant;
using Shop.entities;
using Shop.services;
using System;

namespace Shop.api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/blogs")]
    public class BlogController : GeneralController<Blog, IBlogService>
    {
        private IBlogService blogService;
        private ICommentService commentService;
        private Response response;
        private IUserService userService;

        public BlogController(IBlogService blogService, ICommentService commentService, IUserService userService) : base(blogService)
        {
            this.commentService = commentService;
            this.blogService = blogService;
            this.userService = userService;
            response = new Response();
        }

        [HttpPost("add")]
        public Response Create([FromBody] Blog blog)
        {
            blog.CreatedDate = DateTime.Now.ToString();
            var data = this.blogService.Add(blog);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        [HttpGet("getById")]
        public Response GetById(int blogId)
        {
            var blogDetail = this.blogService.Get(blogId);
            blogDetail.Comments = commentService.GetByBlogId(blogId);
            foreach (var comment in blogDetail.Comments)
            {
                comment.UserName = userService.Get((int)comment.UserId).UserName;
                comment.DateCreated = DateTime.Now.ToString();
            }
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = blogDetail;
            response.Message = "Success";
            return response;
        }
    }
}
