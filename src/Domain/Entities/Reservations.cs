using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Reservation")]
    public class Reservations
    {
        public int Id { get; set; }
        public int TravelerId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int AgentId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int TotalGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public bool Enabled { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual Travelers? Traveler { get; set; }
        public virtual Hotel? Hotel { get; set; }
        public virtual Room? Room { get; set; }
        public virtual LoginUser? Agent { get; set; }


    }
}
