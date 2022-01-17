using Shop.entities;
using System.Collections.Generic;
using System.Linq;

namespace Shop.repositories.RepositoryImpl
{
    public class ProductRepositoryImpl : GeneralRepositoryImpl<Product, DataContext>, IProductRepository
    {
        DataContext _dbContext;
        public ProductRepositoryImpl(DataContext context) : base(context)
        {
            this._dbContext = context;
        }

        public List<Product> GetByBrandId(int brandId)
        {
            if (brandId == 9999)
            {
                return _dbContext.Products.ToList();
            }
            return _dbContext.Products.Where(x => !x.IsDisabled && x.BrandId == brandId).ToList();
        }

        public string GetComposition(int compositionId)
        {
            Composition composition = _dbContext.Compositions.Where(x => x.Id == compositionId).FirstOrDefault();
            if (composition == null)
            {
                return "";
            }
            return composition.Name;
        }
        public List<Product> GetProduct(int brandId, string dataSearch)
        {
            List<Product> products = new List<Product>();
            List<Product> productList = new List<Product>();
            if (brandId == 9999)
            {
                productList = _dbContext.Products.Where(_x => !_x.IsDisabled).ToList();

            }
            else
            {
                productList = _dbContext.Products.Where(_x => _x.BrandId == brandId && !_x.IsDisabled).ToList();
            }


            if (!string.IsNullOrEmpty(dataSearch))
            {
                foreach (Product product in productList)
                {
                    if (GetComposition(product.CompositionId).Equals(dataSearch))
                    {
                        products.Add(product);
                    }

                }
                return products;
            }
            else
            {
                return productList;
            }

        }

        public List<Product> GetProductByName(int brandId, string dataSearch)
        {
            List<Product> productList = _dbContext.Products.Where(_x => _x.BrandId == brandId && !_x.IsDisabled && _x.NameProduct.Contains(dataSearch)).ToList();
            return productList;
        }
    }
}
