
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
        ICartRepository _cartRepository;
        public OrderService() { }
        public OrderService(IOrderRepository repository, IOrderDetailRepository orderDetailRepository, ICartRepository cartRepository) : base(repository)
        { 
            _detailRepository = orderDetailRepository;
            _cartRepository = cartRepository;
            _repository = repository;
        }

        public Order CreateOrder(Order order)
        { 
            Order orderAdded = _repository.Add(order);
            foreach (var item in order.OrderDetails)
            {
                item.DateTrade = DateTime.Now.ToString();
                item.OrderId = orderAdded.Id;
                _detailRepository.CreateOrderDetail(item);
                _cartRepository.Delete(item.Id);
            } 
            return orderAdded; 
        }

        public List<Order> SearchOrderBySdt(string sdt)
        {
            return _repository.SearchOrderBySdt(sdt);
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
                foreach (var detail in orderDetails)
                {
                    detail.Status = o.Status;
                    _detailRepository.Update(detail);
                }
            }
            return order;
        }
    }
}
