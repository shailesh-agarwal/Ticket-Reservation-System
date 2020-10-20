using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenValidateController : ControllerBase
    {
        public IConfiguration _config;
        private systemdbContext _context;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenValidateController));

        public TokenValidateController(IConfiguration config, systemdbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost]
        [Route("api/Token/Login")]
        public IActionResult LoginResult([FromBody] User user)
        {
            _log4net.Info("TokenValidateController - Login");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingUser = _context.PassengerAcc.Where(u => u.PassengerId.ToString() == user.PassengerId).FirstOrDefault();
            
            if (existingUser == null)
            {
                return NotFound();
            }
            
            if (existingUser.PassengerId.ToString() == user.PassengerId && existingUser.PassengerPassword == user.PassengerPassword)
            {
                return Ok(new { token = GenerateJSONWebToken(existingUser) });
            }
            return BadRequest();
        }

        string GenerateJSONWebToken(PassengerAcc user)
        {
            _log4net.Info("TokenValidateController - GenerateToken");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            
            var token = new JwtSecurityToken(_config["Jwt: Issuer"], _config["Jwt: Issuer"], null, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            
            return encodetoken;

        }
    }
}
