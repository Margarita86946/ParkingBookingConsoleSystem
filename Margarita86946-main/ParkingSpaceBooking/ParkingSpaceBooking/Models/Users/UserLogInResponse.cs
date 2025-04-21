namespace ParkingSpaceBooking.Models.Users
{
    public class UserLogInResponse
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int IsAdmin { get; set; }
    }
}