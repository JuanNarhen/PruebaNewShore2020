using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.EntityLayer.Areas.BookingFlow;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.DataLayer.AppDB
{
    // This is the database connection class
    public class AppDbContext : DbContext
    {
        // Default constructor with database options.
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
        }

        // Acces objects for database tables.
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
