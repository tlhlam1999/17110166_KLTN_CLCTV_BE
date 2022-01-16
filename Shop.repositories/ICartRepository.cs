using Shop.entities;
using System.Collections.Generic;

namespace Shop.repositories
{
    public interface ICartRepository : IGeneralRepository<Cart>
    {
        public List<Cart> GetCart(int? userId, string clientIp);
    }
}
