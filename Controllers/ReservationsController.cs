
using Application.Interface;
using Application.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace ApiHotelUltraGroup.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;
        private readonly ITavelersService _tavelersService;

        public ReservationsController(IReservationsService reservationsService, ITavelersService tavelersService)
        {
            _reservationsService = reservationsService;
            _tavelersService = tavelersService;
        }


        [HttpGet("ListAll")]
        public async Task<ActionResult<IEnumerable<Reservations>>> GetAllReservations()
        {
            var reservations = await _reservationsService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<Reservations>> GetReservation(int id)
        {
            var reservation = await _reservationsService.GetReservationByIdAsync(id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        [HttpPost("createReservation")]
        public async Task<ActionResult<Reservations>> CreateReservation([FromBody] Reservations reservation)
        {
            if (reservation == null) return BadRequest();
            var newReservation = await _reservationsService.CreateReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservation), new { id = newReservation.Id }, newReservation);
        }


        [HttpGet("byServiceOwner/{idServiceOwner}")]
        public async Task<IActionResult> GetReservationsAndTravelers(string idServiceOwner)
        {
            var (reservations, travelers) = await _reservationsService.GetReservationsByServiceOwnerAsync(idServiceOwner);

            if ((reservations == null || !reservations.Any()) &&
                (travelers == null || !travelers.Any()))
            {
                return NotFound("No reservations or travelers found.");
            }

            return Ok(new { reservations, travelers });
        }


        [HttpPost("EmailReservation/{email}")]
        public async Task<ActionResult<Reservations>> RecoveryPassword(string email)
        {

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hotelUltraGroup@outlook.com", "No-replay", System.Text.Encoding.UTF8);
            mail.Subject = "Confirmed reservation";
            mail.Body = $@"<html><head><meta charset='UTF-8'><style>
       body {{
           font-family: Arial, sans-serif;
           background-color: #f4f4f4;
           margin: 0;
           padding: 0;
       }}
       .container {{
           background-color: #ffffff;
           width: 100%;
           max-width: 600px;
           margin: 20px auto;
           padding: 20px;
           border-radius: 8px;
           box-shadow: 0 2px 5px rgba(0,0,0,0.1);
       }}
       h1 {{
           font-size: 18px;
           color: #d81111; 
           text-align: center;
       }}
       p {{
           font-size: 14px;
           color: #555;
           line-height: 1.6;
       }}
       .items {{
           margin-top: 10px;
       }}
       .item {{
           background-color: #f9f9f9;
           padding: 10px;
           margin-bottom: 10px;
           border-left: 4px solid #d81111;
       }}
       .logo-container {{
           text-align: left;
           margin: 20px 0;
       }}
       .logo-container img {{
           max-width: 150px;
           height: auto;
       }}</style></head><body><div class='container'><div><h1>Confirmed reservation</h1>
       <p>Hi</p>
       <p>Your reservation has been confirmed</p>
       </div>
       </body>
       </html>";

            mail.To.Add(email);

            mail.IsBodyHtml = true;
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Host = ("smtp-mail.outlook.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("hotelUltraGroup@outlook.com", "dmsdsneapsqdzkck");
            SmtpServer.EnableSsl = true;

            try
            {
                SmtpServer.SendMailAsync(mail);
                return Ok(new { message = "email sent successfully", status = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "", error = ex.Message, status = 503 });
            }
        }
    }
}
