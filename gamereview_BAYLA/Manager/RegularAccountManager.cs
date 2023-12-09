using GameReviewBaylaBusLogic.Context.DBModel;
using GameReviewBaylaModel.Model;
using GameReviewBaylaBusLogic.Context;
using GameReviewBaylaBusLogic.Abstraction;
using IGameReviewBayla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewBaylaBusLogic.Manager
{
    public class RegularAccountManager : UserAccountManager, IRegularAccountTable
    {
        public RegularAccountManager()
        {

        }

        public void InsertReg (User userDetails)
        {
            using (var context = new GameReviewDBContext())
            {
                // entity object creation
                var _regAccount = new RegularAccountTable();
                _regAccount.Id = InsertUser(userDetails);
                _regAccount.First_Name = userDetails.FirstName;
                _regAccount.Last_Name = userDetails.LastName;
                _regAccount.Birthdate = userDetails.BirthDate;

                // add to context
                context.RegularAccountTable.Add(_regAccount);

                // save changes or update to tables
                context.SaveChanges();

                // retrieve data from database (inside context)
                foreach (var s in context.UserAccountInformation)
                {
                    // blank
                }
            }
        }

        private int CalculateAge(DateOnly birth)
        {
            int age;
            return age = DateOnly.FromDateTime(DateTime.Now).Year - birth.Year;
        }

        public void RetrieveRegularInfo (User userDetails)
        {
            using (var _context = new GameReviewDBContext())
            {
                RegularAccountTable details = _context.RegularAccountTable.Where(user => String.Equals(user.Id, userDetails.Id)).First();

                userDetails.FirstName = details.First_Name;
                userDetails.LastName = details.Last_Name;
                userDetails.Age = CalculateAge(details.Birthdate);
            }
        }
    }
}
