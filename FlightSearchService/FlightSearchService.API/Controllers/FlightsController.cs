using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightSearchService.API.Controllers
{
    [ApiController]
    [Route("api/flights")]
    public class FlightsController(SearchFlightsQueryHandler Handler ) : ControllerBase
    {
  
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchFlightsQuery query)
        {
            var result = await Handler.Handle(query);
            return Ok(result);
        }
    }

}
