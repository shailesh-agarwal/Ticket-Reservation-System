using AdminAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPI.Repositories
{
    public class admin : iadmin
    {
        systemdbContext sysdb;
        public admin(systemdbContext _sysdb)
        {
            sysdb = _sysdb;
        }

        public int BookTicket(BookingDetail book)
        {
            int result = 0;
            if (sysdb != null)
            {
                sysdb.BookingDetail.Add(book);
                result = sysdb.SaveChanges();
            }
            return result;
        }

        public List<BookingDetail> GetAllBookingDetail()
        {
            if (sysdb != null)
            {
                return sysdb.BookingDetail.ToList();
            }
            return null;
        }

        public List<PassengerAcc> GetAllPassengerDetail()
        {
            if (sysdb != null)
            {
                return sysdb.PassengerAcc.ToList();
            }
            return null;
        }

        public int RegisterNewPassenger(PassengerAcc user)
        {
            int result = 0;
            if (sysdb != null)
            {
                sysdb.PassengerAcc.Add(user);
                result = sysdb.SaveChanges();
            }
            return result;
        }
    }
}
