using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewBaylaModel.Model
{
    public class Game
    {
        public Guid Id { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Publisher { get; set; }
        public string Year {  get; set; }
        public string Genre { get; set; }
        public string NAsales { get; set; }
        public string EUsales { get; set; }
        public string JPsales { get; set; }
        public string OtherSales { get; set; }
        public double GlobalSales {  get; set; }
    }
}
