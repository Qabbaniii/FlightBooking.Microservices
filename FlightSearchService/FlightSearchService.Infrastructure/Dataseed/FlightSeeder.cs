using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::FlightSearchService.Domain.Entities;
using global::FlightSearchService.Infrastructure.Contexts;

namespace FlightSearchService.Infrastructure.Dataseed
{
    public static class FlightSeeder
    {
        public static async Task SeedAsync(FlightDbContext context)
        {
            if (context.Flights.Any())
                return;

            var flights = new List<Flight>
        {
            new Flight
            {
                Id = Guid.NewGuid(),
                From = "Cairo",
                To = "Alex",
                Date = DateTime.Today.AddDays(1),
                CabinClass = "Economy",
                AvailableSeats = 100,
                Price = 3000
            },
            new Flight
            {
                Id = Guid.NewGuid(),
                From = "ALex",
                To = "Makah",
                Date = DateTime.Today.AddDays(1),
                CabinClass = "Business",
                AvailableSeats = 20,
                Price = 8000
            },
            new Flight
            {
                Id = Guid.NewGuid(),
                From = "Mansoura",
                To = "Post said",
                Date = DateTime.Today.AddDays(2),
                CabinClass = "Economy",
                AvailableSeats = 150,
                Price = 2500
            }
        };

            await context.Flights.AddRangeAsync(flights);
            await context.SaveChangesAsync();
        }
    }
   
}
