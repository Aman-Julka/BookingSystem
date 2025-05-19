using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystemModel.Models
{
    public record BookingResponse
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int InventoryItemId { get; set; }
        public string BookingReference { get; set; }
        public DateTime BookingDateTime { get;  set; }
        public DateTime BookingCancelDateTime { get; set; }
        public bool IsCancelled { get; set; }
    }
}
