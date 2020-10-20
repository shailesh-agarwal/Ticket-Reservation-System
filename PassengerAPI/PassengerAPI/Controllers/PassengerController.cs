using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassengerAPI.Repositories;

namespace PassengerAPI.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class PassengerController : ControllerBase
    {
        ipassenger pass;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PassengerController));

        public PassengerController(ipassenger _pass)
        {
            pass = _pass;
        }

        [HttpGet]
        [Route("api/Passenger/GetTicketType")]
        public IActionResult GetTicketType()
        {
            _log4net.Info("PassengerController - GetTicketType");

            try
            {
                var tickets = pass.GetTicketType();

                if (tickets != null)
                {
                    return Ok(tickets);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Passenger/GetBookingDetail")]
        public IActionResult GetBookingDetail(int? id)
        {
            _log4net.Info("PassengerController - GetBookingDetail");

            try
            {
                var booking = pass.GetBookingDetail(id);

                if (booking != null)
                {
                    return Ok(booking);
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
