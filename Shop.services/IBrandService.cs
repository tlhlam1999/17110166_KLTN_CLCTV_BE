using Shop.entities;
using System.Collections.Generic;

namespace Shop.services
{
    public interface IBrandService : IGeneralService<Brand>
    {
        List<Brand> GetByCategoryId(int categoryId);
    }
}
