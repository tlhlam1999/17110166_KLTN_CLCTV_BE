using Shop.entities;
using System.Collections.Generic;
using System.Linq;

namespace Shop.repositories.RepositoryImpl
{
    public class OrderRepositoryImpl : GeneralRepositoryImpl<Order, DataContext>, IOrderRepository
    {
        DataContext _dbContext;
        public OrderRepositoryImpl(DataContext context) : base(context)
        {
            this._dbContext = context;
        }
         

        public List<Order> SearchOrderByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return _dbContext.Orders.ToList();
            }
            return _dbContext.Orders.Where(x => x.OrderCode.Equals(code)).ToList();
        }
    }
}
