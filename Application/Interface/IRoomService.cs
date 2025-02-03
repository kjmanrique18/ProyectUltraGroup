using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<Room> CreateRoomAsync(Room room);
        Task<bool> UpdateRoomAsync(int id, Room room);
        Task<bool> ToggleRoomStatusAsync(int id);
        Task<List<Room>> GetFilterAsync(DateTime CheckinDate, DateTime CheckoutDate, int Guest, string city);
    }
}
