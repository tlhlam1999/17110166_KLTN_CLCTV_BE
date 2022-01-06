using Shop.entities;
using Shop.repositories;
using System.Collections.Generic;

namespace Shop.services.ServiceImpl
{
    public class BrandService : GeneralServiceImpl<Brand, IBrandRepository>, IBrandService
    {

        IBrandRepository _repository;

        public BrandService() { }
        public BrandService(IBrandRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public List<Brand> GetByCategoryId(int categoryId)
        {
            return _repository.GetByCategoryId(categoryId);
        }
    }
}
