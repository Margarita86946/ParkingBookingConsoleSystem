namespace ParkingSpaceBooking.Queries
{
    public static class SQLCommands
    {
        public const string GetUserByPhone = "select * from users where phone_number = {0}";
        public const string AddUser = "insert into users values ('{0}', '{1}', '{2}', {3}, '{4}')";
        public const string CountUsersBYPhone = "select count(phone_number) from users where phone_number = {0}";

        public const string GetAllSpaces = "select * from spaces";
        public const string GetSpaceByAddress = "select count(id) from spaces where address = '{0}'";
        public const string AddSpace = "insert into spaces values('{0}', {1})";
        public const string RemoveSpace = "delete from spaces where id = {0}";

        public const string GetSpaceIdByAddress = "select id from spaeces where address = {0}";
        public const string CountSlots = "select count(*) from slots where slot_number = {0} and space_id = {1}";
        public const string AddSlot = "insert into slots values ( {0}, {1} )";
        public const string GetSlotesWithAddresses = "select sp.address, sl.slot_number, sp.price from slots sl inner join spaces sp on sp.id = sl.space_id order by sp.address";
        public const string RemoveSlot = "delete from slots where slot_number = {0} and space_id = {1}";

        public const string GetAvailableSlotesForDuration = "SELECT s.*, sp.address, sp.price FROM slots s JOIN spaces sp ON s.space_id = sp.id LEFT JOIN bookings b ON s.id = b.slot_id AND (b.start_date < {1} AND b.end_date > {0}) WHERE b.id IS NULL";
        public const string GetSLotIdBySpaceAndNumber = "select id from slot where space_id = {0} and slot_number = {1}";
        public const string GetSlotByAddress = "SELECT s.id AS slot_id FROM slots s JOIN spaces sp ON s.space_id = sp.id WHERE s.slot_number = {0} AND sp.address = {1}";

        public const string GetUserId = "select id from users where phone_number = '{0}'";
        public const string AddBook = "insert into bookings values ({0}, {1}, '{2}', '{3}')";
    }
}