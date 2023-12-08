using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReviewBaylaModel.Model;

namespace GameReviewBaylaBusLogic.Abstraction
{
    public abstract class ReviewAbstract
    {
        public abstract void CreateReview (Guid reviewID, Guid userID, int Rating, string ReviewText);
        public abstract void GetInfo(Guid g1, Guid g2);
    }
}
