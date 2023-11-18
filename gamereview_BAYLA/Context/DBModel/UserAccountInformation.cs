using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewBaylaBusLogic.Context.DBModel
{
    public class UserAccountInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // mapping Database in class library
        public Guid UserId { get; internal set; }
        public string Username { get; internal set; }
        public string Password { get; internal set; }
    }
}
