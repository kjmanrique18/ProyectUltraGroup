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
    public class TravelersService : ITavelersService
    {
        private readonly HotelContext _context;

        public TravelersService(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<Travelers>> GetAllTravelers()
        {
            return await _context.Traveler.ToListAsync();
        }

        public async Task<Travelers> CreateTravelers(Travelers travelers)
        {
            _context.Traveler.Add(travelers);
            await _context.SaveChangesAsync();
            return travelers;
        }
    }
}
