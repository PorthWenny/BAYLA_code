using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewBaylaBusLogic.Abstraction
{
    public abstract class User
    {
        public abstract bool Insert();
        public string CheckStatus(int id)
        {
            return "Account is active, thank you.";
        }
    }
}