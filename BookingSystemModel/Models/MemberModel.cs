using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemModel.Models
{
    public record MemberModel
    {
        [Name("Id")]
        [Ignore]
        public int Id { get; set; }

        [Name("Name")]
        [Required]
        public string Name { get; set; }

        [Name("Surname")]
        [Required]
        public string Surname { get; set; }

        [Name("BookingCount")]
        [Required]
        public int BookingCount { get; set; }

        [Name("DateJoined")]
        [Required]
        public DateTime DateJoined { get; set; }
    }
}
