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
         

        public List<Order> SearchOrderBySdt(string sdt)
        {
            if (string.IsNullOrEmpty(sdt))
            {
                return _dbContext.Orders.ToList();
            }
            return _dbContext.Orders.Where(x => x.CustomerPhoneNumber.Equals(sdt)).ToList();
        }
    }
}
