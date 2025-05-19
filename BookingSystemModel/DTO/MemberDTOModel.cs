using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookingSystemModel.DTO
{
    public class MemberDTOModel
    {
        [Name("name")]
        [Required]
        public string Name { get; set; }

        [Name("surname")]
        public string Surname { get; set; }

        [Name("booking_count")]
        [Required]
        public int BookingCount { get; set; }

        [Name("date_joined")]
        [Required]
        public DateTime DateJoined { get; set; }
    }
}
