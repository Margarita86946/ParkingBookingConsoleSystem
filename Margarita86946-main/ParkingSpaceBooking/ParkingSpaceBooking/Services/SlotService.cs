using ParkingSpaceBooking.Models.Slots;
using ParkingSpaceBooking.Queries;

namespace ParkingSpaceBooking.Services
{
    class SlotService
    {
        Functionality f = new Functionality();

        public List<GetSlotes> GetAllSlotes()
        {
            List<GetSlotes> slotes = new List<GetSlotes>();

            slotes = f.GetAllSlotes();

            return slotes;
        }

        public Slot AddSlot(AddSlotRequest slot)
        {
            if (f.CheckSpasceExistense(slot.Address))
            {
                int spaceId = f.GetSpaceID(slot.Address);

                if (f.CheckSlotExistense(spaceId, slot.SlotNumber))
                {
                    Console.WriteLine("Slot with this number already exists!");
                    return null;
                }

                Slot addedSlot = f.AddSlot(spaceId, slot.SlotNumber);
                return addedSlot;
            }
            else
                Console.WriteLine("This address does not exist");

            return null;
        }

        public bool RemoveSlot(DeleteSlot slot)
        {
            if (f.CheckSpasceExistense(slot.Address))
            {
                int spaceId = f.GetSpaceID(slot.Address);

                if (f.CheckSlotExistense(spaceId, slot.SlotNumber))
                {
                    f.DeleteSlot(spaceId, slot.SlotNumber);
                    return true;
                }
            }

            return false;
        }

        public int GetSlotId(string spaceAddress, int slotNumber)
        {
            int slotId = f.GetSlotId(spaceAddress, slotNumber);

            return slotId;
        } 
    }
}
