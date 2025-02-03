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
    public class HotelService : IHotelService
    {
        private readonly HotelContext _context;

        public HotelService(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            return await _context.Hotel.ToListAsync();
        }

        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<bool> UpdateHotelAsync(int id, Hotel hotel)
        {
            var existingHotel = await _context.Hotel.FindAsync(id);
            if (existingHotel == null)
                return false;

            existingHotel.HotelName = hotel.HotelName;
            existingHotel.Location = hotel.Location;
            existingHotel.Enabled = hotel.Enabled;
            existingHotel.Favorite = hotel.Favorite;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleHotelStatusAsync(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
                return false;

            hotel.Enabled = !hotel.Enabled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleFavoriteAsync(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
                return false;

            hotel.Favorite = !hotel.Favorite;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> GetLocationAsync()
        {
            return await _context.Hotel
                .Select(h => h.Location)
                .Distinct()
                .ToListAsync();
        }
    }
}
