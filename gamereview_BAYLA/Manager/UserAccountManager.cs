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
        public void LoginUser(User userDetails)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Logging in, showing user info...\n");
            Thread.Sleep(3000);

        }

        public void RegisterUser(User userDetails)
        {
            using (var context = new GameReviewDBContext())
            {
                // entity object creation
                var _userAccount = new UserAccountInformation();
                _userAccount.Username = userDetails.UserName;
                _userAccount.First_name = userDetails.FirstName;
                _userAccount.Last_name = userDetails.LastName;
                _userAccount.Password = userDetails.PassWord;

                // add to context
                context.UserAccountInformation.Add(_userAccount);

                // save changes or update to tables
                context.SaveChanges();

                // retrieve data from database (inside context)
                foreach (var s in context.UserAccountInformation)
                {
                    // blank
                }
            }
        }

        public void RetrieveUser(User userDetails, string uname)
        {
            using (var _context = new GameReviewDBContext())
            {
                UserAccountInformation details = _context.UserAccountInformation.Where(user => String.Equals(user.Username, uname)).First();

                userDetails.FirstName = details.First_name;
                userDetails.LastName = details.Last_name;
                userDetails.UserName = details.Username;
                userDetails.PassWord = details.Password;
            }
        }

        public override bool CheckAvailability (string check_string)
        {
            using (var _context = new GameReviewDBContext())
            {
                bool exists = _context.UserAccountInformation.Any(user => String.Equals(user.Username, check_string));

                return exists;
            }
        }

    }
}
