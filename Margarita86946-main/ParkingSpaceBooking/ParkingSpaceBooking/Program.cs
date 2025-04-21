using ParkingSpaceBooking.Services;

namespace ParkingSpaceBooking;

class Programm
{
    public static void Main(string[] args)
    {
        StartMenu();
    }

    public static void StartMenu()
    {
        Console.WriteLine("Press 1 for log in");
        Console.WriteLine("Press 2 for register");
        int option = int.Parse(Console.ReadLine());

        switch (option)
        {
            case 1:
                MenuService.LogInMenu();
                break;

            case 2:
                MenuService.RegisterMenu();
                break;
        }

        if (MenuService._user.IsAdmin == 1)
            AdminSuccessEnter();
        else
            UserSuccessEnter();
    }

    public static void AdminSuccessEnter()
    {
        Console.WriteLine();
        Console.WriteLine("Press 1 to open space management menu");
        Console.WriteLine("Press 2 to open slot managment menu");
        int option = int.Parse(Console.ReadLine());

        switch (option)
        {
            case 1:
                SpaceMenu();
                break;

            case 2:
                SlotMenu();
                break;
        }
    }

    public static void UserSuccessEnter()
    {
        Console.WriteLine();
        Console.WriteLine("Press 1 to book parking slot for your car.");
        Console.WriteLine("Press 1 to cancel parking slot booking for your car.");
        int option = int.Parse(Console.ReadLine());

        switch (option)
        {
            case 1:
                MenuService.BookSlot();
                break;
        }
    }

    public static void SpaceMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Press 1 to add space");
        Console.WriteLine("Press 2 to remove space");
        int option = int.Parse(Console.ReadLine());

        MenuService.AllSpaces();

        switch (option)
        {
            case 1:
                MenuService.AddSpace();
                break;

            case 2:
                MenuService.DeleteSpace();
                break;
        }
    }

    public static void SlotMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Press 1 to add slot");
        Console.WriteLine("Press 2 to remove slot");
        int option = int.Parse(Console.ReadLine());

        MenuService.AllSlots();

        switch (option)
        {
            case 1:
                MenuService.AddSlot();
                break;

            case 2:
                MenuService.DeleteSlot();
                break;
        }
    }
}