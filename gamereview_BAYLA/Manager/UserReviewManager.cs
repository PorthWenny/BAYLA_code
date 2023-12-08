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
    public class UserReviewManager : ReviewAbstract, IUserReviewManager
    {
        public override void CreateReview(Guid gameID, Guid userID, int rating, string reviewText)
        {
            using (var _context = new GameReviewDBContext())
            {
                var review = new UserReviewTable();

                review.ReviewText = reviewText;
                review.User_ID = userID;
                review.Game_ID = gameID;
                review.Rating = rating;
                review.DateReviewed = DateOnly.FromDateTime(DateTime.Now);

                AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
                _context.UserReviewTable.Add(review);
                _context.SaveChanges();
            }
        }

        public void CreateReview(Guid reviewID, int newRating, string newReviewText)
        {
            using (var _context = new GameReviewDBContext())
            {
                var reviewToEdit = _context.UserReviewTable.FirstOrDefault(r => r.Review_ID == reviewID);

                if (reviewToEdit != null)
                {
                    reviewToEdit.Rating = newRating;
                    reviewToEdit.ReviewText = newReviewText;
                    reviewToEdit.DateReviewed = DateOnly.FromDateTime(DateTime.Now);

                    _context.SaveChanges();
                }
            }
        }

        public bool DeleteReview(Guid reviewID, Guid userID)
        {
            using (var _context = new GameReviewDBContext())
            {
                var reviewToDelete = _context.UserReviewTable.FirstOrDefault(r => r.Review_ID == reviewID && r.User_ID == userID);

                if (reviewToDelete != null)
                {
                    _context.UserReviewTable.Remove(reviewToDelete);
                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public List<Review> RetrieveUserReviews(Guid userId)
        {
            List<Review> userReviews = new List<Review>();

            using (var _context = new GameReviewDBContext())
            {
                List<UserReviewTable> userReviewTables = _context.UserReviewTable.Where(review => review.User_ID == userId).ToList();

                // Assuming Review is your desired model/entity
                foreach (var userReviewTable in userReviewTables)
                {
                    Review reviewDetails = new Review();

                    GetUserReviewInfo(reviewDetails, userReviewTable.Review_ID);

                    userReviews.Add(reviewDetails);
                }
            }

            return userReviews;
        }


        public override void GetInfo(Review reviewDetails, Guid reviewID)
        {
            using (var _context = new GameReviewDBContext())
            {
                var dbreview = _context.UserReviewTable.FirstOrDefault(r => r.Review_ID == reviewID);

                reviewDetails.Review_ID = dbreview.Review_ID;
                reviewDetails.User_ID = dbreview.User_ID;
                reviewDetails.Game_ID = dbreview.Game_ID;
                reviewDetails.ReviewText = dbreview.ReviewText;
                reviewDetails.Rating = dbreview.Rating;
                reviewDetails.DateReviewed = dbreview.DateReviewed;
            }
        }
    }
}
