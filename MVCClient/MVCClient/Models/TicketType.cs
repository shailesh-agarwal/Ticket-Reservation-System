using System;
using System.Collections.Generic;

namespace PassengerAPI.Models
{
    public partial class TicketType
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public double? Price { get; set; }
    }
}
