using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Entities
{
    public class Passenger
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
