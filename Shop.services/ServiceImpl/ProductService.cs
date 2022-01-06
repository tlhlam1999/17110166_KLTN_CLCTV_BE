using Shop.entities;
using Shop.repositories;
using System.Collections.Generic;

namespace Shop.services.ServiceImpl
{
    public class ProductService : GeneralServiceImpl<Product, IProductRepository>, IProductService
    {
        IProductRepository _repository;
        IBrandRepository _brandRepository;

        public ProductService() { }
        public ProductService(IProductRepository repository, IBrandRepository brandRepository) : base(repository)
        {
            _repository = repository;
            _brandRepository = brandRepository;
        }
        public List<Product> GetProduct(int brandId, string dataSearch)
        {
            var products = _repository.GetProduct(brandId, dataSearch);
            foreach (var item in products)
            {
                item.BrandName = _brandRepository.Get(item.BrandId).Name;
            }
            return products;
        }

        public List<Product> GetByBrandId(int brandId)
        {
            var products = _repository.GetByBrandId(brandId);
            foreach (var item in products)
            {
                item.BrandName = _brandRepository.Get(item.BrandId).Name;
            }
            return products;
        }
         
    }
}
