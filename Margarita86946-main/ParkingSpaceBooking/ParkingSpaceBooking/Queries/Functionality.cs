using ParkingSpaceBooking.Models.Bookings;
using ParkingSpaceBooking.Models.Slots;
using ParkingSpaceBooking.Models.Spaces;
using ParkingSpaceBooking.Models.Users;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Reflection.PortableExecutable;

namespace ParkingSpaceBooking.Queries
{
    public class Functionality
    {
        private const string connectionString = "Server=AR\\SQLEXPRESS;Database=parking_space_booking;Trusted_Connection=True;TrustServerCertificate=True;";

        public bool CheckUserExistense(string phoneNumber)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.CountUsersBYPhone, phoneNumber);
            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            int rowCount = (int)command.ExecuteScalar();
            return rowCount == 1;
        }

        public User AddUserToDb(UserRegister user)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.AddUser, user.Name, user.PhoneNumber, user.Password, 0, user.RegisterDate);
            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            command.ExecuteReader();
            User result = new User { PhoneNumber = user.PhoneNumber, Name = user.Name, IsAdmin = 0 };
            return result;
        }

        public UserLogInResponse GetUserByPhone(string phoneNumber)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.GetUserByPhone, phoneNumber);
            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            UserLogInResponse user = new UserLogInResponse { Name = (string)reader["name"], PhoneNumber = (string)reader["phone_number"], Password = (string)reader["password"], IsAdmin = (int)reader["is_admin"] };
            return user;
        }

        public List<Space> GetAllSpaces()
        {
            List<Space> spaces = new List<Space>();

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(SQLCommands.GetAllSpaces, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                Space space = new Space { Id = (int)reader["id"], Address = (string)reader["address"], Price = (int)reader["price"] };
                spaces.Add(space);
            }

            return spaces;
        }

        public bool CheckSpasceExistense(string address)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.GetSpaceByAddress, address);
            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            int rowCount = (int)command.ExecuteScalar();

            return rowCount == 1;
        }

        public Space AddSpaceToDb(AddSpaceRequest space)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.AddSpace, space.Address, space.Price);
            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            command.ExecuteReader();
            Space result = new Space { Address = space.Address, Price = space.Price };
            return result;
        }

        public bool DeleteSpace(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.RemoveSpace, id);

            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            command.ExecuteReader();

            return true;
        } 

        public int GetSpaceID (string address)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.GetSpaceIdByAddress, address);

            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            return (int)reader["id"];

        }

        public bool CheckSlotExistense(int spaceID, int slotNumber)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.CountSlots, slotNumber, spaceID);

            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            int rowCount = (int)command.ExecuteScalar();

            return rowCount == 1;
        }

        public Slot AddSlot(int spaceId, int slotNumber)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.AddSlot, spaceId, slotNumber);

            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            command.ExecuteReader();
            Slot result = new Slot { SlotNumber = slotNumber, SpaceId = spaceId };
            return result;
        }

        public List<GetSlotes> GetAllSlotes()
        {
            List<GetSlotes> slotes = new List<GetSlotes>();
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = SQLCommands.GetSlotesWithAddresses;

            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                GetSlotes slot = new GetSlotes { SpaceAddress = (string)reader["address"], SlotNumber = (int)reader["slotNumber"], Price = (int)reader["price"] };
                slotes.Add(slot);
            }

            return slotes;
        }

        public bool DeleteSlot(int spaceId, int slotNumber)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.RemoveSlot, slotNumber, spaceId);

            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            command.ExecuteReader();

            return true;
        }

        public List<AvailableSlotes> GetAvailableSlotes(DateTime startDate, DateTime endDate)
        {
            List<AvailableSlotes> slots = new List<AvailableSlotes>();

            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.GetAvailableSlotesForDuration, startDate, endDate);

            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                AvailableSlotes slot = new AvailableSlotes { SpaceAddress = (string)reader["address"], SlotNumber = (int)reader["slotNumber"], Price = (int)reader["price"] };
                slots.Add(slot);
            }

            return slots;
        }

        public int GetSlotId(string spaceAddress, int slotNumber)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.GetSlotByAddress, slotNumber, spaceAddress);

            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            return (int)reader["slot_id"];
        }

        public int GetUserId(string phoneNumber)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.GetUserId, phoneNumber);

            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            return (int)reader["id"];
        }

        public bool AddBook(int userId, int slotId, DateTime startDate, DateTime endDate)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string query = string.Format(SQLCommands.AddBook, userId, slotId, startDate, endDate);

            using SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            return true;
        }
    }
}