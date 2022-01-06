using Shop.entities;
using System.Collections.Generic;

namespace Shop.services
{
    public interface IOrderService : IGeneralService<Order>
    {
        List<Order> SearchOrderByCode(string code);
        Order CreateOrder(Order order);
    }
}
