using System;
using System.Collections.Generic;

namespace AuthenticationAPI.Models
{
    public partial class AdminAcc
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public string AdminPassword { get; set; }
    }
}
