using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewBaylaModel.Model
{
    public class Review
    {
        public Guid Review_ID { get; set; }
        public Guid Game_ID { get; set; }
        public Guid User_ID { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateOnly DateReviewed { get; set; }
    }
}
