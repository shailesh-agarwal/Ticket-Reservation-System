using AdminAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPI.Repositories
{
    public interface iadmin
    {
        int RegisterNewPassenger(PassengerAcc user);
        int BookTicket(BookingDetail book);
        List<PassengerAcc> GetAllPassengerDetail();
        List<BookingDetail> GetAllBookingDetail();
    }
}
