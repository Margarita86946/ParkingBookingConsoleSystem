using ParkingSpaceBooking.Models.Bookings;
using ParkingSpaceBooking.Models.Slots;
using ParkingSpaceBooking.Models.Spaces;
using ParkingSpaceBooking.Models.Users;

namespace ParkingSpaceBooking.Services
{
    public static class MenuService
    {
        private static UserService userService = new UserService();
        private static SpaceService spaceService = new SpaceService();
        private static SlotService slotService = new SlotService();
        private static BookingService bookingService = new BookingService();
        public static User _user = null;

        public static void RegisterMenu()
        {
            do
            {
                Console.WriteLine();
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();
                Console.Write("Enter your phone number: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Enter your password: ");
                string password = Console.ReadLine();
                Console.Write("Confirm your password: ");
                string confirmedPassword = Console.ReadLine();

                UserRegister user = new UserRegister
                {
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Password = password,
                    ConfirmPassword = confirmedPassword
                };

                _user = userService.Register(user);

            } while (_user == null);

            Console.WriteLine("Registered successfully!");
        }

        public static void LogInMenu()
        {
            do
            {
                Console.WriteLine();
                Console.Write("Enter your phone number: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Enter your password: ");
                string password = Console.ReadLine();

                UserLogIn user = new UserLogIn
                {
                    Password = password,
                    PhoneNumber = phoneNumber
                };

                _user = userService.LogIn(user);

            } while (_user == null);

            Console.WriteLine("Log in passed successfully!");
        }

        public static void AllSpaces()
        {
            Console.WriteLine();
            Console.WriteLine("All spaces");

            List<Space> spaces = spaceService.GetAllSpaces();

            foreach (var space in spaces)
                Console.WriteLine($"{space.Id}. {space.Address} -> {space.Price}");
        }

        public static void AddSpace()
        {
            if (_user.IsAdmin == 1)
            {
                Space space = null;
                do
                {
                    Console.WriteLine();
                    Console.Write("Enter address for spot: ");
                    string address = Console.ReadLine();

                    Console.Write("Enter price for slots: ");
                    int price = int.Parse(Console.ReadLine());

                    AddSpaceRequest request = new AddSpaceRequest { Address = address, Price = price };

                    space = spaceService.AddSpace(request);
                } while (space == null);

                Console.WriteLine("Space created successfully!");
            }
            else
            {
                Console.WriteLine("You dont have permissions to add space!");
            }
        }

        public static void DeleteSpace()
        {
            if (_user.IsAdmin == 1)
            {
                Console.WriteLine();
                Console.Write("Enter space id you want to delete: ");
                int id = int.Parse(Console.ReadLine());

                if(spaceService.DeleteSpace(id))
                    Console.WriteLine("Space deleted successfully!");
                else
                    Console.WriteLine("Space doesnt deleted!");
            }
            else
                Console.WriteLine("You dont have permissions to remove space!");
        }

        public static void AllSlots()
        {
            Console.WriteLine();
            Console.WriteLine("All slots");

            List<GetSlotes> slots = slotService.GetAllSlotes();

            foreach (var slot in slots)
                Console.WriteLine($"{slot.SpaceAddress} -> {slot.SlotNumber} : {slot.Price}");
        }

        public static void AddSlot()
        {
            if (_user.IsAdmin == 1)
            {
                Slot slot = null;

                do
                {
                    Console.WriteLine("Choose space by its address");
                    string spaceAddress = Console.ReadLine();

                    Console.Write("Enter slot number: ");
                    int slotNumber = int.Parse(Console.ReadLine());

                    AddSlotRequest slotRequest = new AddSlotRequest { Address = spaceAddress, SlotNumber = slotNumber };
                    slot = slotService.AddSlot(slotRequest);
                } while (slot == null);

                Console.WriteLine("Slot added successfully");
            }
            else
                Console.WriteLine("You don't have permission for this action");
        }

        public static void DeleteSlot()
        {
            if (_user.IsAdmin == 1)
            {
                Console.WriteLine("Choose space by its address");
                string spaceAddress = Console.ReadLine();

                Console.Write("Enter slot number: ");
                int slotNumber = int.Parse(Console.ReadLine());

                DeleteSlot slot = new DeleteSlot { Address = spaceAddress, SlotNumber = slotNumber };

                if (slotService.RemoveSlot(slot))
                    Console.WriteLine("Slot deleted successfully!");
                else
                    Console.WriteLine("Slot doesnt deleted!");
            }
            else
                Console.WriteLine("You don't have permission for this action");
        }

        private static (DateTime, DateTime) AvailableSlots()
        {
            DateTime startDate;
            DateTime endDate;

            do
            {
                Console.Write("Enter start date");
                startDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter end date");
                endDate = DateTime.Parse(Console.ReadLine());
            } while (startDate > endDate);
            
            List<AvailableSlotes> slots = bookingService.GetAvailableSlotes(startDate, endDate);

            foreach (var slot in slots)
                Console.WriteLine($"{slot.SpaceAddress} -> {slot.SlotNumber} : {slot.Price}");

            return (startDate, endDate);
        }

        public static void BookSlot()
        {
            Console.WriteLine("Please enter dates you want to book a parking slot.");
            (DateTime startDate, DateTime endDate) = AvailableSlots();

            Console.Write("Enter the space address: ");
            string spaceAddress = Console.ReadLine();

            Console.WriteLine("Enter the slot number: ");
            int slotNumber = int.Parse(Console.ReadLine());

            int slotId = slotService.GetSlotId(spaceAddress, slotNumber);

            AddBooking addBooking = new AddBooking { SlotId = slotId, EndDate = endDate, StartDate = startDate };

            bookingService.Book(addBooking, _user);

            Console.WriteLine($"You booked the slot number {slotNumber} at {spaceAddress} successfully!");
        }
    }
}