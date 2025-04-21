using ParkingSpaceBooking.Models.Spaces;
using ParkingSpaceBooking.Queries;

namespace ParkingSpaceBooking.Services
{
    class SpaceService
    {
        Functionality f = new Functionality();

        public List<Space> GetAllSpaces()
        {
            List<Space> spaceList = new List<Space>();

            spaceList = f.GetAllSpaces();

            return spaceList;
        }

        public Space AddSpace(AddSpaceRequest add)
        {
            if (f.CheckSpasceExistense(add.Address))
            {
                Console.WriteLine("Space with this address already exists!");
                return null;
            }

            Space space = f.AddSpaceToDb(add);

            return space;
        }

        public bool DeleteSpace(int id)
        {
            return f.DeleteSpace(id);
        }
    }
}
