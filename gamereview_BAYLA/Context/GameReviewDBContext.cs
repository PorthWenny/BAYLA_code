using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReviewBaylaBusLogic.Context.DBModel;
using Microsoft.EntityFrameworkCore;
using GameReviewBaylaBusLogic.Context.DBModel;

namespace GameReviewBaylaBusLogic.Context
{
    internal class GameReviewDBContext:DbContext 
    {
        public DbSet<UserAccountInformation> UserAccountInformation {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=PC-Chron0s\\SQLEXPRESS;Database=UserAccountSystem;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;");
        }

    }
}

