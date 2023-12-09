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

        public List<Game> GetGamesList()
        {
            List<Game> gamesList = new List<Game>();

            using (var _context = new GameReviewDBContext())
            {
                // literally had to nerf with .Take since it takes so long...
                var games = _context.GamesTable.Take(1000).ToList();
                int totalGames = games.Count;
                int processedGames = 0;
                const int progressBarWidth = 50;

                Console.WriteLine("Currently reading library. Please wait.\n");

                foreach (var game in games)
                {
                    Game gameDetails = new Game();

                    GetGameInfo(gameDetails, game.Game_ID);

                    gamesList.Add(gameDetails);

                    processedGames++;
                    int progressBarLength = (int)((double)processedGames / totalGames * progressBarWidth);
                    string progressBar = "[" + new string('#', progressBarLength) + new string(' ', progressBarWidth - progressBarLength) + "]";
                    Console.CursorLeft = 0;
                    Console.Write($"Progress: {progressBar} {processedGames}/{totalGames} ({(int)(((double)processedGames / totalGames) * 100)}%)");
                }

                Console.WriteLine("\n\nProcessing complete.");
            }

            return gamesList;
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
                    foreach (var game in games)
                    {
                        Console.WriteLine($"- {game.Name}");
                    }

                    Console.WriteLine("Please refine your search to get a specific game.");
                    return false;
                }
                return false;
            }
        }

    }
}
