using PassengerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassengerAPI.Repositories
{
    public class passenger : ipassenger
    {
        systemdbContext sysdb;
        public passenger(systemdbContext _sysdb)
        {
            sysdb = _sysdb;
        }

        public List<BookingDetail> GetBookingDetail(int? id)
        {
            if (sysdb != null)
            {
                var fetch = from obj in sysdb.BookingDetail where obj.PassengerId == id select obj;
                var fetchlist = fetch.ToList();
                if (fetchlist != null)
                {
                    return fetchlist;
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public List<TicketType> GetTicketType()
        {
            if (sysdb != null)
            {
                return sysdb.TicketType.ToList();
            }
            return null;
        }
    }
}