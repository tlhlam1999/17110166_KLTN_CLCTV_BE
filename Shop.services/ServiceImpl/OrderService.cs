
using Shop.entities;
using Shop.repositories;
using System;
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

        public Order CreateOrder(Order order)
        {

            try
            {
                Order orderAdded = _repository.Add(order);
                foreach (var item in order.OrderDetails)
                {
                    item.DateTrade = DateTime.Now.ToString();
                    item.OrderId = orderAdded.Id;
                    _detailRepository.CreateOrderDetail(item);
                }
                return orderAdded;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public List<Order> SearchOrderByCode(string code)
        {
            return _repository.SearchOrderByCode(code);
        }

        public Order CancelOrder(int id)
        {
            Order order = _repository.Get(id);
            order.Status = 6;
            Order orderUpdated = _repository.Update(order);
            List<OrderDetail> orderDetails = _detailRepository.GetByOrderId(order.Id);
            foreach (var detail in orderDetails)
            {
                detail.Status = 6;
                _detailRepository.Update(detail);
            }
            return orderUpdated;
        }

        public Order UpdateOrder(Order o)
        {
            Order order = _repository.Update(o);
            if (order != null)
            {
                var orderDetails = _detailRepository.GetByOrderId(order.Id);
                foreach(var detail in orderDetails)
                {
                    detail.Status = o.Status; 
                    _detailRepository.Update(detail); 
                }
            }
            return order;
        }
    }
}
