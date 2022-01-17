using System.Collections.Generic;

namespace Shop.services
{
    public interface IGeneralService<T> where T : class
    {
        List<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        T Get(int id);
        T Delete(int id);  
    }
}
