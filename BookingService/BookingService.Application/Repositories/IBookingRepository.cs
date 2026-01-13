using BookingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Repositories
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task SaveChangesAsync();
    }
}
