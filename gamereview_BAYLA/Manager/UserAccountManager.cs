using GameReviewBaylaBusLogic.Context;
using GameReviewBaylaBusLogic.Abstraction;
using GameReviewBaylaBusLogic.Manager;
using GameReviewBaylaBusLogic.Context.DBModel;
using GameReviewBaylaModel.Model;
using IGameReviewBayla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameReviewBaylaBusLogic.Manager
{
    // Initialize class using PascalCase. Create implementation of the interface.
    public class UserAccountManager : UserAbstract, IUserAccountManager
    {
        // Initialize constructor with same name as class, execute w/o functions.
        public UserAccountManager()
        {

        }

        // Initialize function using PascalCase.
        public bool LoginUser (User userDetails, string uname, string pword)
        {
            using (var _context = new GameReviewDBContext())
            {
                bool userExists = _context.UserAccountInformation.Any(user => String.Equals(user.Username, uname));

                if (userExists)
                {
                    UserAccountInformation user = _context.UserAccountInformation.FirstOrDefault(u => u.Username == uname);

                    if (user != null && user.Password == pword)
                    {
                        RetrieveUser(userDetails, uname);
                        return true; // found match
                    }
                }
                return false; // did not match
            }
        }

        public override Guid InsertUser (User userDetails)
        {
            using (var _context = new GameReviewDBContext())
            {
              // entity object creation
                var _userAccount = new UserAccountInformation();
                _userAccount.Username = userDetails.UserName;
                _userAccount.Password = userDetails.PassWord;

                // add to context
                _context.UserAccountInformation.Add(_userAccount);

                // save changes or update to tables
                _context.SaveChanges();

                // retrieve data from database (inside context)
                foreach (var s in _context.UserAccountInformation)
                {
                    // blank
                }

                RetrieveUser(userDetails, _userAccount.Username);

                return _userAccount.UserId = userDetails.Id;
            }

        }

        public void RetrieveUser(User userDetails, string uname)
        {
            using (var _context = new GameReviewDBContext())
            {
                UserAccountInformation details = _context.UserAccountInformation.Where(user => String.Equals(user.Username, uname)).First();

                userDetails.UserName = details.Username;
                userDetails.PassWord = details.Password;
                userDetails.Id = details.UserId;
            }
        }

        public bool CheckAvailability(string check_string)
        {
            using (var _context = new GameReviewDBContext())
            {
                bool exists = _context.UserAccountInformation.Any(user => String.Equals(user.Username, check_string));

                return exists;
            }
        }

    }
}
