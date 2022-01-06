using Shop.entities;
using Shop.repositories; 

namespace Shop.services.ServiceImpl
{ 
    public class BlogService : GeneralServiceImpl<Blog, IBlogRepository>, IBlogService
    {
        IBlogRepository _repository;

        public BlogService() { }
        public BlogService(IBlogRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
