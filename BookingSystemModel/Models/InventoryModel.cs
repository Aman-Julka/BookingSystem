using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookingSystemModel.Models
{
    public record InventoryModel
    {
        [Name("Id")]
        [Ignore]
        public int Id { get; set; }

        [Name("Title")]
        [Required]
        public string Title { get; set; }

        [Name("Description")]
        public string Description { get; set; }

        [Name("RemainingCount")]
        [Required]
        public int RemainingCount { get; set; }

        [Name("ExpirationDate")]
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
