using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class HotelContext : DbContext
    {

        public HotelContext(DbContextOptions<HotelContext> options)
           : base(options)
        {
        }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Reservations> Reservation { get; set; }
        public DbSet<Travelers> Traveler { get; set; }
    }
}
