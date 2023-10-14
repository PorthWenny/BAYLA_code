using GameReviewBaylaModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Interface class, use public initially for web designing.
namespace IGameReviewBayla
{
    public interface IUserAccountManager
    {
        // Redeclare the function into the interface (no implementation).
        public void LoginUser(User userDetails);
        public void RegisterUser(User userDetails);
        public void ShowUser(User userDetails);
    }
}
