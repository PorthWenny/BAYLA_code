using GameReviewBaylaBusLogic.Context;
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
    public class UserAccountManager : IUserAccountManager
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
                _userAccount.Account_Id = userDetails.ID;
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

        public void ShowUser(User userDetails)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine($"     Showing [{userDetails.ID}] User Info      ");
            Console.WriteLine("==============================================");
            Thread.Sleep(1000);
            Console.WriteLine($"Username: {userDetails.UserName}");
            Thread.Sleep(1000);
            Console.WriteLine($"Full Name: {userDetails.FirstName} {userDetails.LastName}");

            Thread.Sleep(2000);
            Console.WriteLine("\nThank you for logging in.");
            Thread.Sleep(2000);
        }
    }
}
