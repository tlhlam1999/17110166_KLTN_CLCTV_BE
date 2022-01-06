using Shop.entities;
using System.Collections.Generic;

namespace Shop.services
{
    public interface IUserService : IGeneralService<User>
    {
        User Login(string username, string password);
        List<Statistical> CaculatorStatistical(string from, string to);
        List<User> GetCustomer();
        User CreateCustomer(User user);

    }
}
