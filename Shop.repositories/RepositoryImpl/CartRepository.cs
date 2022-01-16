using Shop.entities;
using System.Collections.Generic;
using System.Linq;

namespace Shop.repositories.RepositoryImpl
{
    public class CartRepository : GeneralRepositoryImpl<Cart, DataContext>, ICartRepository
    {
        DataContext _dbContext;
        public CartRepository(DataContext context) : base(context)
        {
            this._dbContext = context;
        }
        public List<Cart> GetCart(int? userId, string clientIp)
        {
            var carts = (from cart in _dbContext.Carts
                                where cart.UserId == userId || cart.ClientIp.Equals(clientIp) 
                                select cart).ToList();
            foreach (var cart in carts)
            {
                cart.Product = _dbContext.Products.Where(x => x.Id == cart.ProductId).FirstOrDefault();
                double price = double.Parse(cart.Product.Price.ToString());
                int quantity = int.Parse(cart.Quantity.ToString());
                cart.Balance = price * quantity;
            }
            return carts;
        }
    }
}
