using GameReviewBaylaBusLogic.Manager;
using IGameReviewBayla;
using GameReviewBaylaModel.Model;
using System.Xml.Schema;
using System.Security;
using System.Reflection.Metadata.Ecma335;

User details = new User();

IUserAccountManager userMenu = new UserAccountManager();
IRegularAccountTable regMenu = new RegularAccountManager();
IAdminAccountTable adminMenu = new AdminAccountManager();

Console.WriteLine("==============================================");
Console.WriteLine($"         Good day, Welcome to R8M8!         ");
Console.WriteLine("==============================================\n");
Thread.Sleep(1000);
Console.WriteLine($"[1] Login Account" +
                  $"\n[2] Register Account" +
                  $"\n[3] Exit Program");
Console.WriteLine();
Console.Write($"Input decision: ");

int menuDecision = Convert.ToInt32(Console.ReadLine());

if (menuDecision == 1)
{
    Console.WriteLine("\n==============================================");
    Console.WriteLine($"                 USER LOGIN                   ");
    Console.WriteLine("==============================================\n");

    wronginfo:
    Console.Write("Enter Username: ");
    string uname = Console.ReadLine();
    Console.Write($"Enter Password: ");

    // password input masking
    var pass = string.Empty;
    ConsoleKey key;
    do
    {
        var keyInfo = Console.ReadKey(intercept: true);
        key = keyInfo.Key;

        if (key == ConsoleKey.Backspace && pass.Length > 0)
        {
            Console.Write("\b \b");
            pass = pass[0..^1];
        }
        else if (!char.IsControl(keyInfo.KeyChar))
        {
            Console.Write("*");
            pass += keyInfo.KeyChar;
        }
    } while (key != ConsoleKey.Enter);

    if (!userMenu.LoginUser(details, uname, pass)) {
        Console.WriteLine($"\nUsername or password does not match. Please try again.");
        goto wronginfo;
    }

    if (adminMenu.IsAdmin(details.Id)) {
        adminMenu.RetrieveAdminInfo(details);
    } else{
        regMenu.RetrieveRegularInfo(details);
    }
}
else if (menuDecision == 2) {
    Console.WriteLine("\n==============================================");
    Console.WriteLine($"              REGISTERING USER                ");
    Console.WriteLine("==============================================\n");
    Console.Write($"Enter Username: ");

    // limit the user input username to 20
    string uname = Console.ReadLine();

    while (uname.Length > 20 || uname.Length < 1)
    {
        Console.WriteLine($"Username must be 20 characters max. Please try again.");

        Console.Write("Enter Username: ");
        uname = Console.ReadLine();
    }

    // check if username is still available
    while (userMenu.CheckAvailability(uname))
    {
        Console.WriteLine($"Username is already taken. Please try again.");

        Console.Write("Enter Username: ");
        uname = Console.ReadLine();
    }

    details.UserName = uname;

    Console.Write($"Enter First name: ");
    details.FirstName = Console.ReadLine();
    Console.Write($"Enter Last name: ");
    details.LastName = Console.ReadLine();
    Console.Write($"Enter Password: ");

    // password input masking
    var pass = string.Empty;
    ConsoleKey key;
    do
    {
        var keyInfo = Console.ReadKey(intercept: true);
        key = keyInfo.Key;

        if (key == ConsoleKey.Backspace && pass.Length > 0)
        {
            Console.Write("\b \b");
            pass = pass[0..^1];
        }
        else if (!char.IsControl(keyInfo.KeyChar))
        {
            Console.Write("*");
            pass += keyInfo.KeyChar;
        }
    } while (key != ConsoleKey.Enter);

    details.PassWord = pass;

    Console.WriteLine();
    Console.Write($"Enter birth date (e.g. mm/dd/yyyy): ");
    DateTime utcDateTime = DateTime.SpecifyKind(DateTime.Parse(Console.ReadLine()), DateTimeKind.Utc);

    Thread.Sleep(2000);
    Console.WriteLine();
    Console.WriteLine($"\nThank you, now loggin in.");
    Thread.Sleep(2000);

    regMenu.InsertReg(details);
    userMenu.RetrieveUser(details, uname);
    regMenu.RetrieveRegularInfo(details);
}
else if (menuDecision == 3)
{
    Environment.Exit(0);
}

Console.Clear();

Console.WriteLine("==============================================");
Console.WriteLine($"        Welcome to R8M8. Stay Great.        ");
Console.WriteLine("==============================================");

Thread.Sleep(2000);
Console.WriteLine($"[1] Login Account" +
                  $"\n[2] Register Account" +
                  $"\n[3] Show User Info");
Console.WriteLine();

Console.WriteLine("\ns==============================================");
Console.WriteLine($"     Showing [{details.UserName}] User Info      ");
Console.WriteLine("==============================================");
Thread.Sleep(1000);
Console.WriteLine($"Full Name: {details.FirstName} {details.LastName}");
Thread.Sleep(1000);
Console.WriteLine($"Age: {details.Age}");

Thread.Sleep(2000);