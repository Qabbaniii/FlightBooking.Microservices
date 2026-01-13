using FlightSearchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchService.Application.Repositories
{
    public interface IFlightRepository
    {
        Task<List<Flight>> SearchAsync(SearchFlightsQuery query);
    }
}
