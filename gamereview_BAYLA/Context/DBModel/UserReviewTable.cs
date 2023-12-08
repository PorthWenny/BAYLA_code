using Npgsql.Internal.TypeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviewBaylaBusLogic.Context.DBModel
{
    public class UserReviewTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Review_ID { get; set; }
        public Guid Game_ID { get; set; }
        public Guid User_ID { get; set; }
        public int Rating { get; set; }
        public string ReviewText {  get; set; }
        public DateOnly DateReviewed {  get; set; }
    }
}
