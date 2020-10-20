using System;
using System.Collections.Generic;

namespace PassengerAPI.Models
{
    public partial class PassengerAcc
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public string PassengerPassword { get; set; }
        public string PassengerName { get; set; }
        public string PassengerAddress { get; set; }
        public string PassengerMobile { get; set; }
    }
}
