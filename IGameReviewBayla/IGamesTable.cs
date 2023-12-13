using GameReviewBaylaModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameReviewBayla
{
    public interface IGamesTable
    {
        public List<Game> GetGamesList();
        public bool SearchGameByName(Game gameDetails, string searchInput);
        public void GetGameInfo(Game gameDetails, Guid game_ID);
        public void UpdateGameInfo(Game gameDetails);
        public bool DeleteGame(string searchInput);
    }
}
