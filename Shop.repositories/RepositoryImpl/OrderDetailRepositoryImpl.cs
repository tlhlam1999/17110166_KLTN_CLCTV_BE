using Shop.entities;
using System.Collections.Generic;
using System.Linq;

namespace Shop.repositories.RepositoryImpl
{
    public class OrderDetailRepositoryImpl : GeneralRepositoryImpl<OrderDetail, DataContext>, IOrderDetailRepository
    {
        DataContext _dbContext;
        public OrderDetailRepositoryImpl(DataContext context) : base(context)
        {
            this._dbContext = context;
        }
        public double? GetPrice(int productId)
        {
            var product = _dbContext.Products.Find(productId);
            return product.Price;
        }
        public bool CreateOrderDetail(OrderDetail orderDetail)
        {
            bool isAdded = false;
            try
            {
                var curentOrderDetail = _dbContext.OrderDetails.Find(orderDetail.Id);
                if (curentOrderDetail != null)
                {
                    curentOrderDetail.Balance = curentOrderDetail.Quantity * (double)GetPrice(curentOrderDetail.ProductId);
                    curentOrderDetail.Status = 2;
                    curentOrderDetail.OrderId = orderDetail.OrderId;
                    _dbContext.OrderDetails.Update(curentOrderDetail);
                }
                else
                {
                    _dbContext.OrderDetails.Add(orderDetail);
                }

                isAdded = true;
                _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                isAdded = false;
            }
            return isAdded;
        }

        public List<OrderDetail> GetByOrderId(int orderId)
        {
            return _dbContext.OrderDetails.Where(x => x.OrderId == orderId).ToList();
        }

        public List<OrderDetail> GetOrderDetail(int? userId, string clientIp)
        {
            var orderDetails = (from orderDetail in _dbContext.OrderDetails
                                where orderDetail.UserId == userId || orderDetail.ClientIp.Equals(clientIp)
                                && orderDetail.Status == 1
                                select orderDetail).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.Product = _dbContext.Products.Where(x => x.Id == orderDetail.ProductId).FirstOrDefault();
                double price = double.Parse(orderDetail.Product.Price.ToString());
                int quantity = int.Parse(orderDetail.Quantity.ToString());
                orderDetail.Balance = price * quantity;
            }
            return orderDetails;
        }
    }
}
