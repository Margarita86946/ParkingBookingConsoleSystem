namespace ParkingSpaceBooking.Models.Users
{
    public class UserRegister
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}