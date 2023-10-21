using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewBaylaBusLogic.Abstraction
{
    public abstract class UserAbstract
    {
        public abstract bool CheckAvailability (string check_string);
    }
}