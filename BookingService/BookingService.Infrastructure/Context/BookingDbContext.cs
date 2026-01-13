using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Context
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Status)
                    .HasConversion(v => v.ToString(),v => (BookingStatus)Enum.Parse(typeof(BookingStatus), v))
                    .HasMaxLength(20);
                    
            });
        }
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Passenger> Passengers => Set<Passenger>();
    }
}
