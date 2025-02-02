using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hotel>>> GetAllHotels()
    {
        var hotels = await _hotelService.GetAllHotelsAsync();
        if (hotels == null || hotels.Count == 0)
            return NotFound("No hotels found.");

        return Ok(hotels);
    }

    [HttpPost]
    public async Task<ActionResult<Hotel>> CreateHotel([FromBody] Hotel hotel)
    {
        if (hotel == null || string.IsNullOrEmpty(hotel.HotelName) || string.IsNullOrEmpty(hotel.Location))
            return BadRequest("Hotel name and location are required.");

        var createdHotel = await _hotelService.CreateHotelAsync(hotel);
        return CreatedAtAction(nameof(GetAllHotels), new { id = createdHotel.Id }, createdHotel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] Hotel hotel)
    {
        var result = await _hotelService.UpdateHotelAsync(id, hotel);
        if (!result)
            return NotFound("Hotel not found.");

        return NoContent();
    }

    [HttpPatch("{id}/toggle-status")]
    public async Task<IActionResult> ToggleHotelStatus(int id)
    {
        var result = await _hotelService.ToggleHotelStatusAsync(id);
        if (!result)
            return NotFound("Hotel not found.");

        return NoContent();
    }

    [HttpPatch("{id}/toggle-favorite")]
    public async Task<IActionResult> ToggleHotelFavorite(int id)
    {
        var result = await _hotelService.ToggleFavoriteAsync(id);
        if (!result)
            return NotFound("Hotel not found.");

        return NoContent();
    }

    [HttpGet("locations")]
    public async Task<ActionResult<IEnumerable<string>>> GetLocations()
    {
        var locations = await _hotelService.GetLocationAsync();

        if (locations == null || locations.Count == 0)
            return NotFound("No locations found.");

        return Ok(locations);
    }

}