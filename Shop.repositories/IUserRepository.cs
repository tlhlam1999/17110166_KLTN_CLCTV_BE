using Shop.entities;
using System.Collections.Generic;

namespace Shop.repositories
{
    public interface IUserRepository : IGeneralRepository<User>
    {
        User Login(string username, string password);
        List<Statistical> CaculatorStatistical(string from, string to);
        List<User> GetCustomer();
        User CreateCustomer(User user); 
    }
}
