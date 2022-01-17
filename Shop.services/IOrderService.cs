using Shop.entities;
using System.Collections.Generic;

namespace Shop.services
{
    public interface IOrderService : IGeneralService<Order>
    {
        List<Order> SearchOrderBySdt(string sdt);
        Order CreateOrder(Order order);
        Order CancelOrder(int id);
        Order UpdateOrder(Order order);
    }
}
