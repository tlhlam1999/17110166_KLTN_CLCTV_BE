using Shop.entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Shop.repositories.RepositoryImpl
{
    public class UserRepositoryImpl : GeneralRepositoryImpl<User, DataContext>, IUserRepository
    {
        DataContext _dbContext;
        public UserRepositoryImpl(DataContext context) : base(context)
        {
            this._dbContext = context;
        }

        public double CalculatorRevenue(Product _product)
        {
            double totalRevenue = 0;
            var orderDetails = _dbContext.OrderDetails.ToList();
            var listOrderDetail = orderDetails.Where(x => x.ProductId == _product.Id).ToList();
            foreach (var orderDetail in listOrderDetail)
            {
                totalRevenue += orderDetail.Balance;
            }

            return totalRevenue;
        }

        public int CalculatorSold(Product _product)
        {
            int soldQuantity = 0;
            var orderDetails = _dbContext.OrderDetails.ToList();
            var listOrderDetail = orderDetails.Where(x => x.ProductId == _product.Id).ToList();
            foreach (var orderDetail in listOrderDetail)
            {
                soldQuantity += _product.TotalItems - orderDetail.Quantity;
            }

            return soldQuantity;
        }
        public string GetBrandName(int brandId)
        {
            var brand = _dbContext.Brands.Where(x => x.BrandId == brandId).FirstOrDefault();
            return brand.Name;
        }

        public int CompareTwoDate(DateTime first, DateTime second)
        {
            int res = DateTime.Compare(first, second);
            return res;
        }

        public string ConvertDatetime(string datetime)
        {
            var dateSplited = datetime.Split("-");
            string date = dateSplited[2] + "/" + dateSplited[1] + "/" + dateSplited[0];
            return date;

        }

        public string ConvertDateTrade(string datetime)
        {
            // 1/1/2022 6:42:51 PM
            var dateSplited = datetime.Split(" ")[0].Split("/");
            string day = int.Parse(dateSplited[0].ToString()) < 10 ? "0" + dateSplited[0] : dateSplited[0];
            string month = int.Parse(dateSplited[1]) < 10 ? "0" + dateSplited[1] : dateSplited[1];
            string date = month + "/" + day + "/" + dateSplited[2];
            return date;
        }
        public List<Statistical> CaculatorStatistical(string from, string to)
        {
            List<Product> products = _dbContext.Products.ToList();
            var statisticals = new List<Statistical>();
            foreach (Product product in products)
            {
                var orderDetail = _dbContext.OrderDetails.Where(x => x.ProductId == product.Id).FirstOrDefault();
                if (orderDetail != null)
                {
                    DateTime dateFrom = DateTime.ParseExact(ConvertDatetime(from), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime dateTo = DateTime.ParseExact(ConvertDatetime(to), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime dateTrade = DateTime.ParseExact(ConvertDateTrade(orderDetail.DateTrade), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var fromDateCompared = CompareTwoDate(dateFrom, dateTrade);
                    var toDateCompared = CompareTwoDate(dateTo, dateTrade);
                    if (fromDateCompared <= 0 && toDateCompared >= 0)
                    {
                        var statistical = new Statistical();
                        statistical.Product = product.NameProduct;
                        statistical.Brand = GetBrandName(product.BrandId);
                        statistical.Price = product.Price; 
                        statistical.SoldQuantity = CalculatorSold(product);
                        statistical.Inventory = product.TotalItems - CalculatorSold(product);
                        statistical.TotalRevenue = CalculatorRevenue(product);
                        statisticals.Add(statistical);
                    }
                }

            }
            return statisticals;
        }

        public List<User> GetCustomer()
        {
            var customers = _dbContext.Users.Where(u => u.Role == 2).ToList();
            return customers;
        }

        public User Login(string username, string password)
        {
            var user = _dbContext.Users.Where(u => u.UserName.Equals(username) && u.Password.Equals(password)).FirstOrDefault();
            return user;
        }

        public User CreateCustomer(User user)
        {
            User userResult = new User();
            var result = from u in _dbContext.Users
                         where u.Id == user.Id || u.ClientIp == user.ClientIp
                         select u;
            if (result != null)
            {
                userResult = _dbContext.Users.Add(user).Entity;
            }
            else
            {
                userResult = _dbContext.Users.Update(user).Entity;
            }
            _dbContext.SaveChanges();
            return userResult;
        } 
    }
}
