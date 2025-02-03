
using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiHotelUltraGroup.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // Obtener todas las habitaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            try
            {
                var rooms = await _roomService.GetAllRoomsAsync();

                if (rooms == null || rooms.Count == 0)
                    return NotFound("No rooms found.");

                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Crear una nueva habitación
        [HttpPost]
        public async Task<ActionResult> CreateRoom([FromBody] Room room)
        {
            if (room == null)
            {
                return BadRequest("Room data is required.");
            }

            try
            {
                var createdRoom = await _roomService.CreateRoomAsync(room);
                return CreatedAtAction(nameof(GetAllRooms), new { id = createdRoom.Id }, createdRoom);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Modificar valores de una habitación
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRoom(int id, [FromBody] Room room)
        {
            if (room == null)
            {
                return BadRequest("Room data is required.");
            }

            try
            {
                var updatedRoom = await _roomService.UpdateRoomAsync(id, room);
                if (!updatedRoom)
                {
                    return NotFound($"Room with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPatch("{id}/toggle")]
        public async Task<ActionResult> ToggleRoomStatus(int id)
        {
            try
            {
                var toggled = await _roomService.ToggleRoomStatusAsync(id);
                if (!toggled)
                {
                    return NotFound($"Room with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Room>>> GetFilterGuest([FromQuery] DateTime CheckinDate,[FromQuery] DateTime CheckoutDate,[FromQuery] int Guest,[FromQuery] string city)
        {

            try
            {
                var rooms = await _roomService.GetFilterAsync(CheckinDate, CheckoutDate, Guest, city);

                if (rooms == null || rooms.Count == 0)
                    return NotFound("No rooms found.");

                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
