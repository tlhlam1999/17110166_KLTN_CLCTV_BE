using System.Collections.Generic;
namespace Shop.repositories
{
    public interface IGeneralRepository<T> where T : class
    {
        List<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        T Get(int id);
        T Delete(int id);
    }
}
