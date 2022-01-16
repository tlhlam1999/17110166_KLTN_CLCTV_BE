using Shop.entities;
using System.Linq;

namespace Shop.repositories.RepositoryImpl
{
    public class CompositionRepositoryImpl : GeneralRepositoryImpl<Composition, DataContext>, ICompositionRepository
    {
        DataContext _dbContext;
        public CompositionRepositoryImpl(DataContext context) : base(context)
        {
            this._dbContext = context;
        }

        public Composition GetByName(string name)
        {
            return _dbContext.Compositions.Where(x=>x.Name.Equals(name)).FirstOrDefault();
        }
    }
}
