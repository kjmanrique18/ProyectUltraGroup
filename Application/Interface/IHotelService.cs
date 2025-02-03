using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IHotelService
    {
        Task<List<Hotel>> GetAllHotelsAsync();
        Task<Hotel> CreateHotelAsync(Hotel hotel);
        Task<bool> UpdateHotelAsync(int id, Hotel hotel);
        Task<bool> ToggleHotelStatusAsync(int id);
        Task<bool> ToggleFavoriteAsync(int id);
        Task<List<string>> GetLocationAsync();

    }
}
