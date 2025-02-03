using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Hotel")]
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        public string? HotelName { get; set; }
        public string? Location { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool Favorite { get; set; }
    }
}
