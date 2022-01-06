using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Shop.services;
using Shop.constant;
using Shop.helpers;
using Shop.entities;

namespace Shop.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : GeneralController<User, IUserService>
    {
        private Response response;
        private IUserService _userService;
        private readonly AppSettings _appSettings;
        public UserController(IUserService userService, IOptions<AppSettings> appSettings) : base(userService)
        {
            response = new Response();
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public Response Login([FromBody] Authenticate model)
        {
            var user = _userService.Login(model.Username, model.Password);
            if (user == null) { return response; }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            response.Data = user.WithoutPassword();
            if (user != null)
            {
                response.Status = (int)Configs.STATUS_SUCCESS;
                response.Message = "Success";
            }
            else
            {
                response.Status = (int)Configs.STATUS_ERROR;
                response.Message = "Username or Password invalid";
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public Response Register(User u)
        {
            u.Role = 2;
            var user = _userService.Add(u);
            if (user == null) { return response; }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            response.Data = user.WithoutPassword();
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Message = "Success";
            return response;
        }

        [HttpGet("get-customer")]
        public Response GetCustomer()
        {
            var customers = _userService.GetCustomer();
            response.Data = customers;
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Message = "Success";
            return response;
        }
         
        [HttpPost("caculator-statistical")]
        public Response CaculatorStatistical(string dateFrom, string dateTo)
        {
             var datas = _userService.CaculatorStatistical(dateFrom, dateTo);
            response.Data = datas;
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Message = "Success";
            return response;
        }

        [HttpGet("deactive")]
        public Response DeactiveCustomer(int id)
        {
            var customers = _userService.Delete(id);
            response.Data = customers;
            response.Status = (int)Configs.STATUS_SUCCESS;
            response.Message = "Success";
            return response;
        }
    }
}