using FlightSearchService.Application.DTOs;
using FlightSearchService.Application.Repositories;
using FlightSearchService.Application.Services;

public class SearchFlightsQueryHandler(IFlightRepository Repo,ICacheService Cache)
{


    public async Task<List<FlightDto>> Handle(SearchFlightsQuery query)
    {
        var cacheKey =
            $"FLIGHTS_{query.From}_{query.To}_{query.Date:yyyyMMdd}_{query.CabinClass}_{query.PassengersCount}";

        var cached = await Cache.GetAsync<List<FlightDto>>(cacheKey);
        if (cached != null)
            return cached;

        var flights = await Repo.SearchAsync(query);

        var result = flights.Select(f => new FlightDto
        {
            Id = f.Id,
            From = f.From,
            To = f.To,
            Date = f.Date,
            CabinClass = f.CabinClass,
            Price = f.Price
          
        }).ToList();

        await Cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }
}
