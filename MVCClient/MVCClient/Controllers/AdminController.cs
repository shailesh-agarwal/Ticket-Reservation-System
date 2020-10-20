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
    public class AdminController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AdminController));

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(PassengerAcc newpass)
        {
            _log4net.Info("MVCAdminController - Register");

            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44368/api/Admin/");
                var postPassData = httpclient.PostAsJsonAsync<PassengerAcc>("RegisterNewPassenger", newpass);
                var res = postPassData.Result;
                
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("AllPassenger");
                }
            }
            return View(newpass);
        }

        public IActionResult Book()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Book(BookingDetail newbook)
        {
            _log4net.Info("MVCAdminController - Book");

            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44368/api/Admin/");
                var postBookData = httpclient.PostAsJsonAsync<BookingDetail>("BookTicket", newbook);
                var res = postBookData.Result;

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("AllBooking");
                }
            }
            return View(newbook);
        }

        public async Task<IActionResult> AllPassenger()
        {
            _log4net.Info("MVCAdminController - AllPassenger");

            var allPassenger = new List<PassengerAcc>();

            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44368/api/Admin/");
                HttpResponseMessage res = await httpclient.GetAsync("GetAllPassengerDetail");

                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    allPassenger = JsonConvert.DeserializeObject<List<PassengerAcc>>(result);
                }
            }
            return View(allPassenger);
        }

        public async Task<IActionResult> AllBooking()
        {
            _log4net.Info("MVCAdminController - AllBooking");

            var allBooking = new List<BookingDetail>();

            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44368/api/Admin/");
                HttpResponseMessage res = await httpclient.GetAsync("GetAllBookingDetail");

                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    allBooking = JsonConvert.DeserializeObject<List<BookingDetail>>(result);
                }
            }
            return View(allBooking);
        }
    }
}
