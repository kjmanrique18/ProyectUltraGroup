using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
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
            var room = await _context.Room.FindAsync(reservation.RoomId);

            reservation.TotalPrice = room.BaseCost + room.Taxes;

            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<(List<Reservations>, List<Travelers>)> GetReservationsByServiceOwnerAsync(string idServiceOwner)
        {
            var reservations = await _context.Reservation.Where(r => _context.Traveler.Any(t => t.Id == r.TravelerId && t.IdServiceOwner == idServiceOwner)).ToListAsync();

            var travelers = await _context.Traveler
                .Where(t => t.IdServiceOwner == idServiceOwner)
                .ToListAsync();

            return (reservations, travelers);

        }


    }
}

