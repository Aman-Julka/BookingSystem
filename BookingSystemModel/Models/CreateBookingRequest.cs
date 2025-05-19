namespace BookingSystemModel.Models
{
    public record CreateBookingRequest
    {
        public int MemberId { get; set; }
        public int InventoryItemId { get; set; }
    }
}
