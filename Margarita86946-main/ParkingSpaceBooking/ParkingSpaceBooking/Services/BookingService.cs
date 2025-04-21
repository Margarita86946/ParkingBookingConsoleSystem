using ParkingSpaceBooking.Models.Bookings;
using ParkingSpaceBooking.Models.Spaces;
using ParkingSpaceBooking.Models.Users;
using ParkingSpaceBooking.Queries;

namespace ParkingSpaceBooking.Services
{
    class BookingService
    {
        Functionality f = new Functionality();

        public List<AvailableSlotes> GetAvailableSlotes(DateTime startDate, DateTime endDate)
        {
            List<AvailableSlotes> spaceList = new List<AvailableSlotes>();

            spaceList = f.GetAvailableSlotes(startDate, endDate);

            return spaceList;
        }

        public bool Book(AddBooking add, User user)
        {
            int userId = f.GetUserId(user.PhoneNumber);

            f.AddBook(userId, add.SlotId, add.StartDate, add.EndDate);

            return true;
        }
    }
}
