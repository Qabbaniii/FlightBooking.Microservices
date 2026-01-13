using BookingService.Application.DTOs;
using BookingService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController(ICreateBookingHandler handler) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingRequest request)
    {
        var bookingId = await handler.HandleAsync(request);
        return Ok(new { BookingId = bookingId });
    }
}
