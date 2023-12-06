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
        public bool LoginUser(User userDetails, string uname, string pword);
        public Guid InsertUser(User userDetails);
        public void RetrieveUser(User userDetails, string uname);
        public bool CheckAvailability(string check_string);
    }
}
