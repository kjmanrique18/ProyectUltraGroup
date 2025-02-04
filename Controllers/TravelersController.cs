using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiHotelUltraGroup.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TravelersController : ControllerBase
    {
        private readonly ITavelersService _tavelersService;

        public TravelersController(ITavelersService tavelersService)
        {
            _tavelersService = tavelersService;
        }

        [HttpGet("listAll")]
        public async Task<ActionResult<IEnumerable<Travelers>>> GetAll()
        {
            var travelers = await _tavelersService.GetAllTravelers();
            if (travelers == null || travelers.Count == 0)
                return NotFound("No travels found.");

            return Ok(travelers);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Travelers>> CreateTraveler([FromBody] Travelers travelers)
        {
            if (travelers == null || string.IsNullOrEmpty(travelers.IdServiceOwner) || string.IsNullOrEmpty(travelers.FullName) || string.IsNullOrEmpty(travelers.DocumentNumber))
                return BadRequest("Id service owner and full name and document number are required.");

            var createdTraveler = await _tavelersService.CreateTravelers(travelers);
            return CreatedAtAction(nameof(GetAll), new { id = createdTraveler.Id }, createdTraveler);
        }

    }
}
