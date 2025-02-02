using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
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
