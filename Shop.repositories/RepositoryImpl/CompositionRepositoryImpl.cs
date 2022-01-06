using Shop.entities;

namespace Shop.repositories.RepositoryImpl
{
    public class CompositionRepositoryImpl : GeneralRepositoryImpl<Composition, DataContext>, ICompositionRepository
    {
        DataContext _dbContext;
        public CompositionRepositoryImpl(DataContext context) : base(context)
        {
            this._dbContext = context;
        }
 
    }
}
