using Shop.entities;
using System.Collections.Generic;

namespace Shop.services
{
    public interface IProductService : IGeneralService<Product>
    {
        public List<Product> GetByBrandId(int brandId);
        public List<Product> GetProduct(int brandId, string dataSearch);
        public List<Product> GetProductByName(int brandId, string? name);
    }
}
