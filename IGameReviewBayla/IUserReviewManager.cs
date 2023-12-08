using GameReviewBaylaModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGameReviewBayla
{
    public interface IUserReviewManager
    {
        public void CreateReview (Guid gameID, int rating, string reviewText);
        public void CreateReview(Guid gameID, Guid userID, int rating, string reviewText);
        public bool DeleteReview(Guid reviewID, Guid userID);
        public List<Review> RetrieveUserReviews(Guid userId);
        public void GetUserReviewInfo(Review reviewDetails, Guid reviewID);

    }
}
