using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReviewBaylaBusLogic.Context.DBModel;
using Microsoft.EntityFrameworkCore;

namespace GameReviewBaylaBusLogic.Context
{
    internal class GameReviewDBContext: DbContext
    {
        public DbSet<UserAccountInformation> UserAccountInformation {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("User Id=postgres;Password=g4qEJzFtavGBcSkS;Server=db.aktytbzvpldtdbxujshl.supabase.co;Port=5432;Database=postgres");
        }

    }
}

