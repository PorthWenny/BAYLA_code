using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewBaylaModel.Model
{
    // contain variables or entities in Model.
    public class User
    {
        // treat as Object entity, use PascalCase.
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassWord { get; set; }
        public DateOnly BirthDate { get; set; }
        public int Age { get; set; }
    }
}
