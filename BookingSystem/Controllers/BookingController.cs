using BookingSystem.BAL.Interfaces;
using BookingSystemModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Route("Booking")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingBAL _bookingBAL;
        public BookingController(IBookingBAL bookingBAL)
        {
            _bookingBAL = bookingBAL;
        }

        [HttpPost("book")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            try
            {
                BookingResponse booking = await _bookingBAL.CreateBooking(request);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelBooking([FromBody] CreateBookingRequest request)
        {
            try
            {
                BookingResponse booking = await _bookingBAL.CancelBooking(request);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
