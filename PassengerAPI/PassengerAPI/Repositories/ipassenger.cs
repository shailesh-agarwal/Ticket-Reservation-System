using PassengerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerAPI.Repositories
{
    public interface ipassenger
    {
        List<TicketType> GetTicketType();
        List<BookingDetail> GetBookingDetail(int? id);
    }
}
