using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly HotelContext _context;

        public RoomService(HotelContext context)
        {
            _context = context;
        }

        // Obtener todas las habitaciones
        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _context.Room.Include(x => x.Hotel).ToListAsync();
        }

        // Crear una nueva habitación
        public async Task<Room> CreateRoomAsync(Room room)
        {
            _context.Room.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        // Actualizar habitación
        public async Task<bool> UpdateRoomAsync(int id, Room room)
        {
            var existingRoom = await _context.Room.FindAsync(id);
            if (existingRoom == null)
                return false;

            existingRoom.RoomType = room.RoomType;
            existingRoom.BaseCost = room.BaseCost;
            existingRoom.Taxes = room.Taxes;
            existingRoom.Location = room.Location;
            existingRoom.MaxGuest = room.MaxGuest;

            await _context.SaveChangesAsync();
            return true;
        }

        // Habilitar o deshabilitar habitación
        public async Task<bool> ToggleRoomStatusAsync(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room == null)
                return false;

            room.Enabled = !room.Enabled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Room>> GetFilterAsync(DateTime CheckinDate, DateTime CheckoutDate, int Guest, string city)
        {
            var roomsQuery = _context.Room
           .Include(r => r.Hotel)
           .Where(r => r.Hotel.Location == city && r.MaxGuest >= Guest);

            var reservedRoomIds = await _context.Reservation
                .Where(res => res.CheckInDate < CheckoutDate && res.CheckOutDate > CheckinDate)
                .Select(res => res.RoomId)
                .ToListAsync();

            var availableRooms = await roomsQuery
                .Where(r => !reservedRoomIds.Contains(r.Id))
                .ToListAsync();

            return availableRooms;
        }
    }
}
