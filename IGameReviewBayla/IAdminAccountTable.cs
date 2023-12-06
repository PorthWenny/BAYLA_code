using GameReviewBaylaModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameReviewBayla
{
    public interface IAdminAccountTable
    {
        public void RetrieveAdminInfo(User userDetails);

        public bool IsAdmin(Guid userId);
    }
}
