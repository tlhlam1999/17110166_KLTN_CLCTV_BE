using Shop.entities;
using System.Collections.Generic;

namespace Shop.repositories
{
    public interface IBrandRepository : IGeneralRepository<Brand>
    {
        List<Brand> GetByCategoryId(int categoryId); 
    }
}
