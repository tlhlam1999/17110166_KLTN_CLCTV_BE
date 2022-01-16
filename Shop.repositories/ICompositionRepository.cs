using Shop.entities;

namespace Shop.repositories
{
    public interface ICompositionRepository : IGeneralRepository<Composition>
    {
        Composition GetByName(string name);
    }
}
