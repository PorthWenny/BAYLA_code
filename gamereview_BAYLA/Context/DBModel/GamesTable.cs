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
    public class GamesTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Game_ID { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Publisher { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public string NA_Sales { get; set; }
        public string EU_Sales { get; set; }
        public string JP_Sales { get; set; }
        public string Other_Sales { get; set; }
        public double Global_Sales { get; set; }
    }
}
