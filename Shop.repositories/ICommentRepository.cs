using Shop.entities;
using System.Collections.Generic;

namespace Shop.repositories
{
    public interface ICommentRepository : IGeneralRepository<Comment>
    {
        List<Comment> GetCommentByBlogId(int blogId);
    }
}
