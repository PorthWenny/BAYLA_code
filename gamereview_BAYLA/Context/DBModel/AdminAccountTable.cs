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
    public class AdminAccountTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateOnly Birthdate { get; set; }
    }
}
