using Shop.entities;
using System.Collections.Generic;

namespace Shop.services
{
    public interface ICartService : IGeneralService<Cart>
    {
        public List<Cart> GetCart(int? userId, string clientIp);
    }
}
