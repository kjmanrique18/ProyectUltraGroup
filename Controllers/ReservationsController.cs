
using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiHotelUltraGroup.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;

        public ReservationsController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservations>>> GetAllReservations()
        {
            var reservations = await _reservationsService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservations>> GetReservation(int id)
        {
            var reservation = await _reservationsService.GetReservationByIdAsync(id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult<Reservations>> CreateReservation([FromBody] Reservations reservation)
        {
            if (reservation == null) return BadRequest();
            var newReservation = await _reservationsService.CreateReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservation), new { id = newReservation.Id }, newReservation);
        }
    }
}
