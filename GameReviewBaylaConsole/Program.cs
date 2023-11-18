﻿using GameReviewBaylaBusLogic.Manager;
using IGameReviewBayla;
using GameReviewBaylaModel.Model;
using System.Xml.Schema;
using System.Security;

User details = new User();

IUserAccountManager userMenu = new UserAccountManager();
IRegularAccountTable regMenu = new RegularAccountManager();

Console.WriteLine("==============================================");
Console.WriteLine($"       Good day, Welcome to PlayPal          ");
Console.WriteLine("==============================================\n");
Thread.Sleep(1000);
Console.WriteLine($"[1] Login Account" +
                  $"\n[2] Register Account");
Console.WriteLine();
Console.Write($"Input decision: ");

int menuDecision = Convert.ToInt32(Console.ReadLine());

if (menuDecision == 2) {
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
    Console.WriteLine($"\nThank you, showing user info now.");
    Thread.Sleep(2000);

    regMenu.InsertReg(details);
    userMenu.RetrieveUser(details, uname);
    regMenu.RetrieveRegularInfo(details);
}

Console.WriteLine("==============================================");
Console.WriteLine($"     Showing [{details.UserName}] User Info      ");
Console.WriteLine("==============================================");
Thread.Sleep(1000);
Console.WriteLine($"Full Name: {details.FirstName} {details.LastName}");
Thread.Sleep(1000);
Console.WriteLine($"Age: {details.Age}");

Thread.Sleep(2000);
Console.WriteLine("\nThank you for logging in.");
Thread.Sleep(2000);