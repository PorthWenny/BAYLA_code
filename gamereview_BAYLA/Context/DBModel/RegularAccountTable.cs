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
    public class RegularAccountTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; internal set; }
        public string First_Name { get; internal set; }
        public string Last_Name { get; internal set; }
        public DateTime Birthdate { get; internal set; }
    }
}
