using AuthenticationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAPI.Repositories
{
    public interface iauthentication
    {
        public PassengerAcc AuthenticateUser(User user);
        public string GenerateJSONWebToken(PassengerAcc passengerAcc);
    }
}
