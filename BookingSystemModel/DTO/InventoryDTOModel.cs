using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookingSystemModel.DTO
{
    public class InventoryDTOModel
    {
        [Name("title")]
        [Required]
        public string Title { get; set; }

        [Name("description")]
        public string Description { get; set; }

        [Name("remaining_count")]
        [Required]
        public int RemainingCount { get; set; }

        [Name("expiration_date")]
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
