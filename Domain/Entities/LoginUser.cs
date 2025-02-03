using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("User")]
    public class LoginUser
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? DocumentNumber { get; set; }
        public bool Enabled { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? UserType { get; set; }
    }
}
