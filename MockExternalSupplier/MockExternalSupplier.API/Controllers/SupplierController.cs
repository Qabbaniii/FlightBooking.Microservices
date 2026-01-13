using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs;
namespace MockExternalSupplier.API.Controllers
{
    [ApiController]
    [Route("api/supplier")]
    public class SupplierController : ControllerBase
    {
        [HttpGet("availability/{flightId}")]
        public IActionResult CheckAvailability(Guid flightId,[FromQuery] int count)
        {
            var isAvailable = count <= 5;
            return Ok(new AvailabilityResponse
            {
                IsAvailable = isAvailable
            });
        }

        [HttpGet("confirmBooking/{bookingId}")]
        public async Task<IActionResult> ConfirmBooking(Guid bookingId)
        {
           
            await Task.Delay(500);
            var isConfirmed = Random.Shared.Next(0, 2) == 1;
          
            return Ok(new BookingConfirmationResponse
            {
                IsConfirmed = isConfirmed
            });
        }
    }

}
