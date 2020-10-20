using System;
using System.Collections.Generic;

namespace AdminAPI.Models
{
    public partial class AdminAcc
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public string AdminPassword { get; set; }
    }
}
