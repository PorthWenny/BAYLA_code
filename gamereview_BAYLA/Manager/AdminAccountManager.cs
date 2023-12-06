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
    public class AdminAccountManager : UserAccountManager, IAdminAccountTable
    {
        public AdminAccountManager()
        {

        }

        private int CalculateAge(DateTime birth)
        {
            int age;
            return age = DateTime.Now.Year - birth.Year;
        }

        public void RetrieveAdminInfo(User userDetails)
        {
            using (var _context = new GameReviewDBContext())
            {
                AdminAccountTable details = _context.AdminAccountTable.Where(user => String.Equals(user.Id, userDetails.Id)).First();

                userDetails.FirstName = details.First_Name;
                userDetails.LastName = details.Last_Name;
                userDetails.Age = CalculateAge(details.Birthdate);
            }
        }

        public bool IsAdmin(Guid userId)
        {
            using (var _context = new GameReviewDBContext())
            {
                return _context.AdminAccountTable.Any(user => Guid.Equals(user.Id, userId));
            }
        }
    }
}
