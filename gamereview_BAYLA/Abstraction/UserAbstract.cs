using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReviewBaylaModel.Model;

namespace GameReviewBaylaBusLogic.Abstraction
{
    public abstract class UserAbstract
    {
        public abstract Guid InsertUser (User userDetails);

    }
}