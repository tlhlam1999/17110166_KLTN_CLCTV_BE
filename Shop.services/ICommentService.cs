using Shop.entities;
using System.Collections.Generic;

namespace Shop.services
{
    public interface ICommentService : IGeneralService<Comment>
    {
        List<Comment> GetByBlogId(int blogId);
    }
}
