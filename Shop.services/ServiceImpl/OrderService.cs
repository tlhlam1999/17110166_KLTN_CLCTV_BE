
using Shop.entities;
using Shop.repositories;
using System.Collections.Generic;

namespace Shop.services.ServiceImpl
{
    public class OrderService : GeneralServiceImpl<Order, IOrderRepository>, IOrderService
    {
        IOrderRepository _repository;
        IOrderDetailRepository _detailRepository;
        IProductRepository _productRepository;
        public OrderService() { }
        public OrderService(IOrderRepository repository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository) : base(repository)
        {
            _productRepository = productRepository;
            _detailRepository = orderDetailRepository;
            _repository = repository;
        }

        public double? GetPrice(int productId)
        {
            var product = _productRepository.Get(productId);
            return product.Price;
        }
        public Order CreateOrder(Order order)
        {
         
            Order orderAdded = _repository.Add(order);
            foreach (var item in order.OrderDetails)
            {
                item.Balance = item.Quantity * (double)GetPrice(item.ProductId);
                item.Status = 2;
                item.OrderId = orderAdded.Id;
                _detailRepository.CreateOrderDetail(item);
            }
            return orderAdded;
        }

        public List<Order> SearchOrderByCode(string code)
        {
            return _repository.SearchOrderByCode(code);
        }
    }
}
