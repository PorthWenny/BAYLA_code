using GameReviewBaylaBusLogic.Manager;
using IGameReviewBayla;
using GameReviewBaylaModel.Model;
using System.Xml.Schema;
using System.Security;
using System.Reflection.Metadata.Ecma335;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Transactions;

AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

logout:
User details = new User();
Game gameDetails = new Game();
Review reviewDetails = new Review();

IUserAccountManager userMenu = new UserAccountManager();
IRegularAccountTable regMenu = new RegularAccountManager();
IAdminAccountTable adminMenu = new AdminAccountManager();
IGamesTable manageGames = new GamesTableManager();
IUserReviewManager reviewMenu = new UserReviewManager();

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
    Console.Write("Enter birth date (e.g. yyyy-MM-dd): ");
    details.BirthDate = DateOnly.Parse(Console.ReadLine());

    Thread.Sleep(2000);
    Console.WriteLine();
    Console.WriteLine($"\nThank you, now logging in.");
    Thread.Sleep(2000);

    regMenu.InsertReg(details);
    userMenu.RetrieveUser(details, details.Id);
    regMenu.RetrieveRegularInfo(details);
}
else if (menuDecision == 3)
{
    Environment.Exit(0);
}
else
{
    Console.WriteLine();
    Console.Write($"Invalid input. Please try again...");
    Console.ReadKey();
    Console.Clear();
    goto logout;
}

Console.Clear();

ratemenu:
Console.WriteLine("==============================================");
Console.WriteLine($"        Welcome to R8M8. Stay Great.        ");
Console.WriteLine("==============================================\n");

Thread.Sleep(1000);
Console.WriteLine($"[1] View Games" +
                  $"\n[2] Create Review" +
                  $"\n[3] Edit History" +
                  $"\n[4] Show User Information" +
                  $"\n[5] Logout" +
                  $"\n[6] Exit Program");

if (adminMenu.IsAdmin(details.Id))
{
    Console.WriteLine($"\n[ADMIN PRIVILEGE MENU]" +
                  $"\n[7] Delete a Review" +
                  $"\n[8] Edit Games Library");
}

Console.WriteLine();
Console.Write($"Input decision: ");

int user_Decision = Convert.ToInt32(Console.ReadLine());

if (user_Decision == 1)
{
    Console.WriteLine("\n==============================================");
    Console.WriteLine("             R8M8'S GAME LIBRARY             ");
    Console.WriteLine("==============================================\n");
    Thread.Sleep(1000);

    const int pageSize = 10;
    int currentIndex = 0;
    List<Game> gamesList = manageGames.GetGamesList();

    while (true)
    {
        int count = Math.Min(pageSize, gamesList.Count - currentIndex);

        if (count == 0)
        {
            Console.WriteLine("No more games to display.");
            break;
        }

        for (int i = currentIndex; i < currentIndex + count; i++)
        {
            Console.WriteLine($"[{i + 1}]  '{gamesList[i].Name}' by {gamesList[i].Publisher} - Rank: {gamesList[i].Rank}");
        }

        Console.WriteLine("Commands: 'next' to show next 10 games, 'back' to clear console,");
        Console.WriteLine("or any valid number to show 10 games from the specified index.");
        Console.Write("Enter command: ");

        string input = Console.ReadLine();

        if (input == "next")
        {
            currentIndex += pageSize;
            manageGames.GetGamesList();
        }
        else if (input == "back")
        {
            Console.Clear();
            goto ratemenu;
        }
        else
        {
            // Try to parse the entire input as an integer
            try
            {
                string[] inputs = input.Split(' ');
                int index = int.Parse(string.Join("", inputs));

                if (index >= 1 && index <= gamesList.Count)
                {
                    currentIndex = (index - 1) % pageSize;
                    Console.WriteLine($"\nYou've moved to game {index} in the list.");
                }
                else
                {
                    Console.WriteLine("Invalid index. Please enter a valid number.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter 'next', 'back', or a valid number.");
            }
        }
    }
}
else if (user_Decision == 2)
{
    retrySearch:
    Console.WriteLine("\n==============================================");
    Console.WriteLine($"               LET'S GO R8, M8.              ");
    Console.WriteLine("==============================================");
    Thread.Sleep(1000);

    Console.WriteLine("\nEnter the name of the game to search: ");
    string searchInput = Console.ReadLine();

    if (!manageGames.SearchGameByName(gameDetails, searchInput))
    {
        goto retrySearch;
    }
;
    Console.Write("Enter the Rating (1-10): ");
    int rating;
    while (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 10)
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 10 for the rating.");
        Console.Write("Enter the Rating (1-10): ");
    }

    Console.Write("Enter your Review Text: ");
    string reviewText = Console.ReadLine();

    // Create a review using the provided information
    reviewMenu.CreateReview(gameDetails.Id, details.Id, rating, reviewText);

    Console.WriteLine("\nReview created successfully!");
    Console.ReadKey();
    Console.Clear();
    goto ratemenu;
}
else if (user_Decision == 3)
{
    Console.WriteLine("\n==============================================");
    Console.WriteLine($"     Showing [{details.UserName}] History    ");
    Console.WriteLine("==============================================\n");
    Thread.Sleep(1000);

    List<Review> userReviews = reviewMenu.RetrieveUserReviews(details.Id);

    int listCount = 0;
    foreach (var review in userReviews)
    {
        nevermind:
        Console.WriteLine($"----------------------[{listCount+1}]-------------------------");
        manageGames.GetGameInfo(gameDetails, review.Game_ID);
        Console.WriteLine($"Game: {gameDetails.Name}");
        Console.WriteLine($"Rating: {review.Rating}");
        Console.WriteLine($"Review Text: {review.ReviewText}");
        Console.WriteLine($"Date Reviewed: {review.DateReviewed}");
        Console.WriteLine("--------------------------------------------------");

        Console.Write("(E)dit or (d)elete? (enter any key to continue.): ");
        string input = Console.ReadLine();

        if (input == "E" || input == "e")
        {
            Console.Write("Enter new rating (1-10): ");
            int rating;
            while (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 10)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 10 for the rating.");
                Console.Write("Enter new rating (1-10): ");
            }

            Console.Write("Enter your new review: ");
            string newReviewText = Console.ReadLine();

            reviewMenu.CreateReview(review.Review_ID, rating, newReviewText);
            Console.Write("\nReview has been edited. \n");
            Thread.Sleep(1000);
        }
        else if (input == "D" || input == "d")
        {
            restartSure:
            Console.Write("\n Are you sure? (Y/N) ");
            string sureInput = Console.ReadLine();

            if (sureInput.ToUpper() == "Y")
            {
                reviewMenu.DeleteReview(review.Review_ID, details.Id);
                Console.WriteLine("\nReview deleted. \n");
            }
            else if (sureInput.ToUpper() == "N")
            {
                // If 'N' or 'n' is entered, go back to a specific label using goto
                Console.WriteLine("\nUnderstood. Going back. \n");
                goto nevermind;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'Y' or 'N'.");
                goto restartSure;
            }
        }
        else
        {
            continue;
        }
        
        listCount++;
    }

    Thread.Sleep(2000);
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
    Console.Clear();
    goto ratemenu;

}
else if (user_Decision == 4) {
    Console.WriteLine("\ns==============================================");
    Console.WriteLine($"     Showing [{details.UserName}] User Info      ");
    Console.WriteLine("==============================================");
    Thread.Sleep(1000);
    Console.WriteLine($"Full Name: {details.FirstName} {details.LastName}");
    Thread.Sleep(1000);
    Console.WriteLine($"Age: {details.Age}");

    Thread.Sleep(2000);
    Console.WriteLine($"\nPress any key to continue...");
    Console.ReadKey();
    Console.Clear();
    goto ratemenu;
}
else if (user_Decision == 5)
{
    Console.WriteLine($"\nLogging out. Please wait...");
    Thread.Sleep(3000);
    Console.Clear();
    goto logout;
}
else if (user_Decision == 6)
{
    Environment.Exit(0);
}
else if (user_Decision == 7 && adminMenu.IsAdmin(details.Id))
{
    Console.WriteLine("\n==============================================");
    Console.WriteLine($"           Showing Review History            ");
    Console.WriteLine("==============================================\n");
    Thread.Sleep(1000);

    List<Review> userReviews = reviewMenu.RetrieveAllUserReviews();

    Guid initialId = details.Id;

    int listCount = 0;
    foreach (var review in userReviews)
    {
        Console.WriteLine($"----------------------[{listCount + 1}]-------------------------");
        userMenu.RetrieveUser(details, review.User_ID);
        Console.WriteLine($"RATED BY: {details.UserName}");
        manageGames.GetGameInfo(gameDetails, review.Game_ID);
        Console.WriteLine($"Game: {gameDetails.Name}");
        Console.WriteLine($"Rating: {review.Rating}");
        Console.WriteLine($"Review Text: {review.ReviewText}");
        Console.WriteLine($"Date Reviewed: {review.DateReviewed}");
        Console.WriteLine("--------------------------------------------------");

        listCount++;
    }

    restartDel:
    Console.Write("Enter an index to delete, [0] to go back: ");
    int index = Convert.ToInt32(Console.ReadLine());

    if (index >= 1 && index <= listCount)
    {
        reviewMenu.DeleteReview(userReviews[index-1].Review_ID, userReviews[index-1].User_ID);
        Console.WriteLine($"\nReview has been deleted. Thank you.\n");
        Console.ReadKey();
    }
    else if (index == 0)
    {
        Console.Write("Going back to menu.\n");
    }
    {
        Console.WriteLine($"\nReview [{index}] not found. Please input a valid index.\n");
        goto restartDel;
    }

    Thread.Sleep(2000);
    Console.WriteLine("\nPress any key to continue...");
    userMenu.RetrieveUser(details, initialId);
    Console.ReadKey();
    Console.Clear();
    goto ratemenu;
}
else if (user_Decision == 8 && adminMenu.IsAdmin(details.Id))
{
    retrySearch:
    Console.WriteLine("\n==============================================");
    Console.WriteLine($"              Editing Game Library           ");
    Console.WriteLine("==============================================");
    Thread.Sleep(1000);

    Console.WriteLine("\nEnter the name of the game to search: ");
    string searchInput = Console.ReadLine();
    Console.WriteLine("\n");

    if (!manageGames.SearchGameByName(gameDetails, searchInput))
    {
        goto retrySearch;
    }

    manageGames.GetGameInfo(gameDetails, gameDetails.Id);

    Console.WriteLine($"\n------------------[GAME INFO]---------------------");
    Console.WriteLine($"Title: {gameDetails.Name}");
    Console.WriteLine($"Rank: {gameDetails.Rank}");
    Console.WriteLine($"Publisher and Year: {gameDetails.Publisher}, {gameDetails.Year}");
    Console.WriteLine($"Genre: {gameDetails.Genre}");
    Console.WriteLine($"Platform: {gameDetails.Platform}");
    Console.WriteLine("-----------------------------------------------------");

    restartEdit:
    Console.WriteLine("\nWhat information do you want to edit? " +
        "\nOtherwise, type 'Delete' to delete the game or 'Back' to go back. " +
        "\nInput decision: ");
    string typeEdit = Console.ReadLine().ToLower();

    if (typeEdit == "title")
    {
        Console.WriteLine("Input new title: ");
        string newTitle = Console.ReadLine();

        gameDetails.Name = newTitle;
        manageGames.UpdateGameInfo(gameDetails);
        Console.WriteLine("Title updated successfully!");
    }
    else if (typeEdit == "rank")
    {
        Console.WriteLine("Input new rank: ");
        if (int.TryParse(Console.ReadLine(), out int newRank))
        {
            gameDetails.Rank = newRank;
            manageGames.UpdateGameInfo(gameDetails);
            Console.WriteLine("Rank updated successfully!");
        }
        else
        {
            Console.WriteLine("Invalid input. Rank must be a number.");
        }
    }
    else if (typeEdit == "publisher")
    {
        Console.WriteLine("Input new publisher: ");
        string newPublisher = Console.ReadLine();

        gameDetails.Publisher = newPublisher;
        manageGames.UpdateGameInfo(gameDetails);
        Console.WriteLine("Publisher updated successfully!");
    }
    else if (typeEdit == "delete")
    {
        if (!manageGames.DeleteGame(gameDetails.Name))
        {
            goto restartEdit;
        }
    }
    else if (typeEdit == "back")
    {
        Console.Clear();
        goto ratemenu;
    }
    else
    {
        Console.WriteLine("Invalid input for editing. Please try again.");
        goto restartEdit;
    }

    Thread.Sleep(2000);
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
    Console.Clear();
    goto ratemenu;
}
else
{
    Console.WriteLine("\nInvalid input. Please try again.");
    Console.ReadKey();
    Console.Clear();
    goto ratemenu;
}