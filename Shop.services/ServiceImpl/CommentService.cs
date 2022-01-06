using Shop.entities;
using Shop.repositories;
using System.Collections.Generic;

namespace Shop.services.ServiceImpl
{
    public class CommentService : GeneralServiceImpl<Comment, ICommentRepository>, ICommentService
    {

        ICommentRepository _repository;

        public CommentService() { }
        public CommentService(ICommentRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public List<Comment> GetByBlogId(int blogId)
        {
            return _repository.GetCommentByBlogId(blogId);
        }
    }
}
