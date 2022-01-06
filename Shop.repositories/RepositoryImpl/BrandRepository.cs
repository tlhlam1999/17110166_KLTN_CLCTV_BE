using Shop.entities;
using System.Collections.Generic;
using System.Linq;

namespace Shop.repositories.RepositoryImpl
{
    public class BrandRepositoryImpl : GeneralRepositoryImpl<Brand, DataContext>, IBrandRepository
    {
        DataContext _dbContext;
        public BrandRepositoryImpl(DataContext context) : base(context)
        {
            _dbContext = context;
        }

        public List<Brand> GetByCategoryId(int categoryId)
        {
            var brands = _dbContext.Brands.Where(x => x.CategoryId == categoryId).ToList();
            return brands;
        }
    }
}
