using Shop.entities;
using Shop.repositories;
using System.Collections.Generic;

namespace Shop.services.ServiceImpl
{
    public class CartService : GeneralServiceImpl<Cart, ICartRepository>, ICartService
    {
        ICartRepository _repository;

        public CartService() { }
        public CartService(ICartRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public List<Cart> GetCart(int? userId, string clientIp)
        {
            return _repository.GetCart(userId, clientIp);
        }
    }
}
