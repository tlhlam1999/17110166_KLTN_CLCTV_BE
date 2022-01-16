using Shop.entities;
using Shop.repositories; 

namespace Shop.services.ServiceImpl
{
    public class CompositionService : GeneralServiceImpl<Composition, ICompositionRepository>, ICompositionService
    {
        ICompositionRepository _repository;

        public CompositionService() { }
        public CompositionService(ICompositionRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Composition GetByName(string name)
        {
            return _repository.GetByName(name);
        }
    }
}
