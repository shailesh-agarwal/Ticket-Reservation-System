using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminAPI.Models;
using AdminAPI.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminAPI.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AdminController : ControllerBase
    {
        iadmin adm;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AdminController));

        public AdminController(iadmin _adm)
        {
            adm = _adm;
        }

        [HttpPost]
        [Route("api/Admin/RegisterNewPassenger")]
        public IActionResult RegisterNewPassenger([FromBody] PassengerAcc user)
        {
            _log4net.Info("AdminController - RegisterNewPassenger");

            try
            {
                var register = adm.RegisterNewPassenger(user);
                if (register > 0)
                {
                    return Ok(register);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/Admin/BookTicket")]
        public IActionResult BookTicket([FromBody] BookingDetail book)
        {
            _log4net.Info("AdminController - BookTicket");

            try
            {
                var booking = adm.BookTicket(book);
                if (booking > 0)
                {
                    return Ok(booking);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Admin/GetAllPassengerDetail")]
        public IActionResult GetAllPassengerDetail()
        {
            _log4net.Info("AdminController - GetAllPassengerDetail");

            try
            {
                var allpass = adm.GetAllPassengerDetail();

                if (allpass != null)
                {
                    return Ok(allpass);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Admin/GetAllBookingDetail")]
        public IActionResult GetAllBookingDetail()
        {
            _log4net.Info("AdminController - GetAllBookingDetail");

            try
            {
                var allbook = adm.GetAllBookingDetail();

                if (allbook != null)
                {
                    return Ok(allbook);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
