using Shop.entities;
using System.Collections.Generic;
using System.Linq;

namespace Shop.repositories.RepositoryImpl
{
    public class CommentRepositoryImpl : GeneralRepositoryImpl<Comment, DataContext>, ICommentRepository
    {
        DataContext _dbContext;
        public CommentRepositoryImpl(DataContext context) : base(context)
        {
            _dbContext = context;
        }

        public List<Comment> GetCommentByBlogId(int blogId)
        {
            return _dbContext.Comments.Where(x => x.BlogId == blogId).ToList();
        }
    }
}
