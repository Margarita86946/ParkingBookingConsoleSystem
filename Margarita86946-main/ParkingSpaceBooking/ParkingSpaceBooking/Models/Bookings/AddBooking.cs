namespace ParkingSpaceBooking.Models.Bookings
{
    public class AddBooking
    {
        public int SlotId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
