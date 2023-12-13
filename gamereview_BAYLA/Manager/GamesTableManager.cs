using GameReviewBaylaBusLogic.Context.DBModel;
using GameReviewBaylaBusLogic.Context;
using GameReviewBaylaModel.Model;
using IGameReviewBayla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewBaylaBusLogic.Manager
{
    public class GamesTableManager : IGamesTable
    {
        public GamesTableManager() { }

        private const int PageSize = 10;
        private int currentPage = 1;
        private List<Game> gamesList = new List<Game>();
        private int totalGamesCount = 0;

        public List<Game> GetGamesList()
        {
            using (var _context = new GameReviewDBContext())
            {
                if (gamesList.Count == 0 || gamesList.Count >= totalGamesCount)
                {
                    gamesList.Clear();
                    totalGamesCount = _context.GamesTable.Count(); 
                    currentPage = 1;
                }

                var games = _context.GamesTable.Skip((currentPage - 1) * PageSize).Take(PageSize).ToList();
                currentPage++;

                foreach (var game in games)
                {
                    Game gameDetails = new Game();
                    GetGameInfo(gameDetails, game.Game_ID);
                    gamesList.Add(gameDetails);
                }

                return gamesList;
            }
        }

        public void GetGameInfo (Game gameDetails, Guid game_ID)
        {
            using (var _context = new GameReviewDBContext())
            {
                var game = _context.GamesTable.FirstOrDefault(g => g.Game_ID == game_ID);

                gameDetails.Id = game.Game_ID;
                gameDetails.Rank = game.Rank;
                gameDetails.Name = game.Name;
                gameDetails.Platform = game.Platform;
                gameDetails.Publisher = game.Publisher;
                gameDetails.Year = game.Year;
                gameDetails.Genre = game.Genre;
                gameDetails.NAsales = game.NA_Sales;
                gameDetails.JPsales = game.JP_Sales;
                gameDetails.EUsales = game.EU_Sales;
                gameDetails.OtherSales = game.Other_Sales;
                gameDetails.GlobalSales = game.Global_Sales;
            }
        }

        public void UpdateGameInfo(Game gameDetails)
        {
            using (var _context = new GameReviewDBContext())
            {
                var game = _context.GamesTable.FirstOrDefault(g => g.Game_ID == gameDetails.Id);

                if (game != null)
                {
                    game.Name = gameDetails.Name;
                    game.Rank = gameDetails.Rank;
                    game.Platform = gameDetails.Platform;
                    game.Publisher = gameDetails.Publisher;
                    game.Year = gameDetails.Year;
                    game.Genre = gameDetails.Genre;
                    // Update if possible.

                    _context.SaveChanges();
                }
            }
        }


        public bool SearchGameByName(Game gameDetails, string searchInput)
        {
            using (var _context = new GameReviewDBContext())
            {
                var games = _context.GamesTable
                    .Where(g => g.Name.Contains(searchInput))
                    .ToList();

                if (games.Count == 0)
                {
                    Console.WriteLine("No matching games found.");
                    return false;
                }

                if (games.Count == 1)
                {
                    Console.WriteLine($"Found game: {games[0].Name}");

                    // Ask the user if this is the game they intended
                    Console.Write("Is this the game you meant? (yes/no): ");
                    string userResponse = Console.ReadLine().ToLower();

                    if (userResponse == "yes")
                    {
                        Guid gameID = games[0].Game_ID;

                        gameDetails.Id = gameID;
                        return true;
                    }
                    else if (userResponse == "no")
                    {
                        Console.WriteLine("No further action taken.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
                else // Multiple games found
                {
                    Console.WriteLine($"Multiple games found. Do you mean any of these?");
                    for (int i = 0; i < games.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {games[i].Name}");
                    }

                    Console.Write("Select an index or [0] if none: ");
                    int selectedIndex;
                    bool isValidIndex = int.TryParse(Console.ReadLine(), out selectedIndex);

                    if (isValidIndex && selectedIndex >= 0 && selectedIndex <= games.Count)
                    {
                        if (selectedIndex == 0)
                        {
                            Console.WriteLine("No further action taken.");
                        }
                        else
                        {
                            Guid gameID = games[selectedIndex - 1].Game_ID;
                            gameDetails.Id = gameID;
                            return true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid index or input.");
                    }
                }
                return false;
            }
        }

        public bool DeleteGame(string searchInput)
        {
            using (var _context = new GameReviewDBContext())
            {
                var gamesToDelete = _context.GamesTable
                    .Where(g => g.Name.Contains(searchInput))
                    .ToList();

                if (gamesToDelete.Count == 0)
                {
                    Console.WriteLine("No matching games found.");
                    return false;
                }

                if (gamesToDelete.Count == 1)
                {
                    var gameToDelete = gamesToDelete[0];

                    Console.Write("Are you sure you want to delete this game? (yes/no): ");
                    string userResponse = Console.ReadLine().ToLower();

                    if (userResponse == "yes")
                    {
                        _context.GamesTable.Remove(gameToDelete);
                        _context.SaveChanges();
                        Console.WriteLine("Game deleted successfully.");
                        return true;
                    }
                    else if (userResponse == "no")
                    {
                        Console.WriteLine("No further action taken.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
                else
                {
                    Console.WriteLine($"Multiple games found. Please specify the index of the game to delete:");
                    for (int i = 0; i < gamesToDelete.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {gamesToDelete[i].Name}");
                    }

                    Console.Write("Select an index or [0] if none: ");
                    int selectedIndex;
                    bool isValidIndex = int.TryParse(Console.ReadLine(), out selectedIndex);

                    if (isValidIndex && selectedIndex >= 0 && selectedIndex <= gamesToDelete.Count)
                    {
                        if (selectedIndex == 0)
                        {
                            Console.WriteLine("No further action taken.");
                        }
                        else
                        {
                            var gameToDelete = gamesToDelete[selectedIndex - 1];

                            Console.Write("Are you sure you want to delete this game? (yes/no): ");
                            string userResponse = Console.ReadLine().ToLower();

                            if (userResponse == "yes")
                            {
                                _context.GamesTable.Remove(gameToDelete);
                                _context.SaveChanges();
                                Console.WriteLine("Game deleted successfully.");
                                return true;
                            }
                            else if (userResponse == "no")
                            {
                                Console.WriteLine("No further action taken.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid input.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid index or input.");
                    }
                }

                return false;
            }
        }
    }
}
