using Shop.repositories; 
using System.Collections.Generic;

namespace Shop.services.ServiceImpl
{
    public abstract class GeneralServiceImpl<TEntity, TRepository>
    where TEntity : class
    where TRepository : IGeneralRepository<TEntity>
    {
        private readonly TRepository repository;
        public GeneralServiceImpl() { }
        public GeneralServiceImpl(TRepository repository)
        {
            this.repository = repository;
        }

        public TEntity Add(TEntity entity)
        {
            return repository.Add(entity);
        }

        public List<TEntity> GetAll()
        {
            return repository.GetAll();
        }

        public TEntity Update(TEntity entity)
        {
            return repository.Update(entity);
        }

        public TEntity Get(int id)
        {
            return repository.Get(id);
        }

        public TEntity Delete(int id)
        {
            return repository.Delete(id);
        }
    }
}
