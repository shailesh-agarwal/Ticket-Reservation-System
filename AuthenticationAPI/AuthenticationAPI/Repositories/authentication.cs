using AuthenticationAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Repositories
{
    public class authentication : iauthentication
    {
        private IConfiguration _config;
        private systemdbContext _sysdb;
        public authentication(IConfiguration config, systemdbContext sysdb)
        {
            _config = config;
            _sysdb = sysdb;
        }
        public PassengerAcc AuthenticateUser(User user)
        {
            PassengerAcc passenger = null;

            List<PassengerAcc> allpassenger = _sysdb.PassengerAcc.ToList();
            
            foreach (var v in allpassenger)
            {
                int passId = int.Parse(user.PassengerId);
                
                if (v.PassengerId == passId && v.PassengerPassword == user.PassengerPassword)
                {
                    passenger = new PassengerAcc {PassengerId = passId, PassengerPassword = user.PassengerPassword};
                }
            }
            return passenger;
        }

        public string GenerateJSONWebToken(PassengerAcc passengerAcc)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
