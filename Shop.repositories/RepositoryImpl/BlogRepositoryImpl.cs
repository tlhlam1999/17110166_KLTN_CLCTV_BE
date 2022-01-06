using Shop.entities;

namespace Shop.repositories.RepositoryImpl
{
    public class BlogRepositoryImpl : GeneralRepositoryImpl<Blog, DataContext>, IBlogRepository
    {
        DataContext _dbContext;
        public BlogRepositoryImpl(DataContext context) : base(context)
        {
            this._dbContext = context;
        }
 
    }
}
