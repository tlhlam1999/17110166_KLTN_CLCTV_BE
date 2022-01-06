using Shop.entities;
using Shop.repositories; 

namespace Shop.services.ServiceImpl
{
    public class CategoryService : GeneralServiceImpl<Category, ICategoryRepository>, ICategoryService
    {

        ICategoryRepository _repository;

        public CategoryService() { }
        public CategoryService(ICategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
