using ParkingSpaceBooking.Models.Users;
using ParkingSpaceBooking.Queries;
using System.Text;
using System.Security.Cryptography;

namespace ParkingSpaceBooking.Services
{
    class UserService
    {
        Functionality f = new Functionality();
        public User Register(UserRegister userRegister)
        {
            if (f.CheckUserExistense(userRegister.PhoneNumber))
            {
                Console.WriteLine("User with this number already exists");
                return null;
            }

            if (!string.Equals(userRegister.ConfirmPassword, userRegister.Password))
            {
                Console.WriteLine("Your confirmed password does not match the original password");
                return null;
            }
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(userRegister.Password);
            byte[] hash = sha256.ComputeHash(bytes);
            userRegister.Password = Convert.ToBase64String(hash);

            return f.AddUserToDb(userRegister);
        }

        public User LogIn(UserLogIn userLogIn)
        {
            if (!f.CheckUserExistense(userLogIn.PhoneNumber)) 
            {
                Console.WriteLine("User with this number does not exist");
                return null;
            }

            UserLogInResponse user = f.GetUserByPhone(userLogIn.PhoneNumber);
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(userLogIn.Password);
            byte[] hash = sha256.ComputeHash(bytes);
        
            if (!string.Equals(Convert.ToBase64String(hash), user.Password))
            {
                Console.WriteLine("You entered wrong password");
                return null;
            }

            User result = new User { Name = user.Name, PhoneNumber = user.PhoneNumber, IsAdmin = user.IsAdmin };
            return result;
        }

    }
}