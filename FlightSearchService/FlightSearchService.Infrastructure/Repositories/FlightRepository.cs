using FlightSearchService.Application.Repositories;
using FlightSearchService.Domain.Entities;
using FlightSearchService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

public class FlightRepository(FlightDbContext context) : IFlightRepository
{


    public async Task<List<Flight>> SearchAsync(SearchFlightsQuery query)
    {
        return await context.Flights
            .Where(f =>
                f.From == query.From &&
                f.To == query.To &&
                f.Date.Date == query.Date.Date &&
                f.CabinClass == query.CabinClass &&
                f.AvailableSeats >= query.PassengersCount)
            .ToListAsync();
    }
}
