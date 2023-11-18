using GameReviewBaylaModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameReviewBayla
{
    public interface IRegularAccountTable
    {
        public void InsertReg (User userDetails);
        public void RetrieveRegularInfo (User userDetails);
    }
}
