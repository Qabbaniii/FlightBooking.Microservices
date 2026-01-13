using BookingService.Application.Repositories;
using BookingService.Domain.Entities;
using BookingService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Repositories
{
    public class BookingRepository(BookingDbContext Context) : IBookingRepository
    {

        public async Task AddAsync(Booking booking)
        {
            await Context.Bookings.AddAsync(booking);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
