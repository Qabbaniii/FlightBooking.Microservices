using BookingService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces
{
    public interface ICreateBookingHandler
    {
        public Task<Guid> HandleAsync(CreateBookingRequest request);
    }
}
