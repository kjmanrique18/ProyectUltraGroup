using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly HotelContext _context;

        public ReservationsService(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<Reservations>> GetAllReservationsAsync()
        {
            return await _context.Reservation.ToListAsync();
        }

        public async Task<Reservations> GetReservationByIdAsync(int id)
        {
            return await _context.Reservation.Include(x => x.Traveler)
                .Include(x => x.Hotel)
                .Include(x => x.Room)
                .Include(x => x.Agent)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Reservations> CreateReservationAsync(Reservations reservation)
        {


            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
    }
}
