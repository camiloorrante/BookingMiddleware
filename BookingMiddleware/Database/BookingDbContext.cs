using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BookingMiddleware.Models;


namespace BookingMiddleware.Database
{
    public class BookingDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<City> Cities { get; set; }

        public BookingDbContext() : base()
        {

        }
    }
}