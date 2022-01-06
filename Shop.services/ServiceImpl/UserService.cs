
using Shop.entities;
using Shop.repositories;
using System.Collections.Generic;

namespace Shop.services.ServiceImpl
{
    public class UserService : GeneralServiceImpl<User, IUserRepository>, IUserService
    {
        IUserRepository _repository;

        public UserService() { }
        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public List<Statistical> CaculatorStatistical(string from, string to)
        {
            return _repository.CaculatorStatistical(from, to);
        }

        public User CreateCustomer(User user)
        {
            return _repository.CreateCustomer(user);
        }
 
        public List<User> GetCustomer()
        {
            var customers = _repository.GetCustomer();
            return customers;

        }

        public User Login(string username, string password)
        {
            var user = _repository.Login(username, password);
            return user;
        }

    }
}
