using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationAPI.Models;
using AuthenticationAPI.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AuthenticationController : ControllerBase
    {
        iauthentication auth;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthenticationController));
        public AuthenticationController(iauthentication _auth)
        {
            auth = _auth;
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            _log4net.Info("AuthenticationController - Login");

            IActionResult response = Unauthorized();
            
            var passenger = auth.AuthenticateUser(user);
            
            if (passenger != null)
            {
                var authString = auth.GenerateJSONWebToken(passenger);

                response = Ok(authString);
            }
            return response;
        }
    }
}
