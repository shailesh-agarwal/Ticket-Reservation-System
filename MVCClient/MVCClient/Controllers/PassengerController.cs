using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PassengerAPI.Models;

namespace MVCClient.Controllers
{
    public class PassengerController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PassengerController));

        public async Task<IActionResult> TicketType()
        {
            _log4net.Info("MVCPassengerController - TicketType");

            var lst = new List<TicketType>();

            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44377/api/Passenger/");
                HttpResponseMessage res = await httpclient.GetAsync("GetTicketType");
                
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    lst = JsonConvert.DeserializeObject<List<TicketType>>(result);
                }
            }
            return View(lst);
        }

        public async Task<IActionResult> BookingDetail(int id)
        {
            _log4net.Info("MVCPassengerController - BookingDetail");

            var detail = new List<BookingDetail>();

            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44377/api/Passenger/");
                HttpResponseMessage res = await httpclient.GetAsync("GetBookingDetail?id=" + id);

                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    detail = JsonConvert.DeserializeObject<List<BookingDetail>>(result);
                }
            }
            return View(detail);
        }
    }
}
