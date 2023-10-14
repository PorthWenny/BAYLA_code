using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewBaylaBusLogic.Context.DBModel
{
    public class UserAccountInformation
    {
        [Key]
        // mapping Database in class library
        public int Account_Id { get; internal set; }
        public string First_name { get; internal set; }
        public string Last_name { get; internal set; }
        public string Username { get; internal set; }
        public string Password { get; internal set; }
    }
}
