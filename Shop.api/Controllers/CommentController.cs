using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.constant;
using Shop.entities;
using Shop.services;

namespace Shop.api.Controllers
{
    [Authorize]
    [ApiController]

    [Route("api/comments")]
    public class CommentController : GeneralController<Comment, ICommentService>
    {
        private ICommentService commentService;
        private IUserService userService;
        private Response response;
        public CommentController(ICommentService commentService, IUserService userService) : base(commentService)
        {
            this.userService = userService;
            this.commentService = commentService;
            response = new Response();
        }

        [HttpPost("add")]
        public Response Create([FromBody] Comment comment)
        {
            comment.UserName = userService.Get((int)comment.UserId).UserName;
            comment.DateCreated = DateTime.Now.ToString();
            var data = this.commentService.Add(comment);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        [HttpGet("getByBlogId")]
        public Response GetByBlogId(int blogId)
        {
            var data = this.commentService.GetByBlogId(blogId);
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

    }
}
