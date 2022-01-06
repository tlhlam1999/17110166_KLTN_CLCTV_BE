using Shop.entities;
using System.Collections.Generic;

namespace Shop.repositories
{
    public interface IOrderDetailRepository : IGeneralRepository<OrderDetail>
    {
        public List<OrderDetail> GetByOrderId(int orderId);
        public List<OrderDetail> GetOrderDetail(int? userId, string clientIp);
        public bool CreateOrderDetail(OrderDetail orderDetail);
    }
}
