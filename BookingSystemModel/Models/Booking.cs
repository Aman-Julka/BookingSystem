using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemModel.Models
{
    public record Booking
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int InventoryItemId { get; set; }
        public DateTime BookingDateTime { get; set; }
        public bool IsCancelled { get; set; }
    }
}
