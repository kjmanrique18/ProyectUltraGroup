using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Room")]
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string? RoomType { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string? Location { get; set; }
        public bool Enabled { get; set; } = true;
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public int MaxGuest { get; set; }

        public virtual Hotel? Hotel { get; set; }
    }
}
