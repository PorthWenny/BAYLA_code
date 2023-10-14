using GameReviewBaylaBusLogic.Manager;
using IGameReviewBayla;
using GameReviewBaylaModel.Model;
using System.Xml.Schema;
using System.Security;

User details = new User();
details.UserName = "PorthWenny";
details.FirstName = "Angelo";
details.LastName = "Bayla";

Console.WriteLine("==============================================");
Console.WriteLine($"       Good day, Welcome to PlayPal          ");
Console.WriteLine("==============================================");
Thread.Sleep(1000);
Console.WriteLine($"Initial login detected, please register first.");
Console.WriteLine();
Console.Write($"Enter Username: ");

// limit the user input username to 20
string uname = Console.ReadLine();

while (uname.Length > 20 || uname.Length < 1)
{
    Console.WriteLine($"Username must be 20 characters max. Please try again.");

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


Thread.Sleep(2000);
Console.WriteLine();
Console.WriteLine($"\nThank you, redirecting to login page.");
Thread.Sleep(2000);

IUserAccountManager menu = new UserAccountManager();

menu.RegisterUser(details);
menu.LoginUser(details);
menu.ShowUser(details);