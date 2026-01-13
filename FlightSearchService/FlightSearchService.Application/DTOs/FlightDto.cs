using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchService.Application.DTOs
{
    public class FlightDto
    {
    public Guid Id { get; set; }
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public DateTime Date { get; set; }
    public string CabinClass { get; set; } = null!;
    public decimal Price { get; set; }
    }
}
