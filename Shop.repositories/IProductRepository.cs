using Shop.entities;
using System.Collections.Generic;

namespace Shop.repositories
{
    public interface IProductRepository : IGeneralRepository<Product>
    {
        public List<Product> GetByBrandId(int brandId);
        public List<Product> GetProduct(int brandId, string dataSearch);
    }
}
