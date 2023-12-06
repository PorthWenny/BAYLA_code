using GameReviewBaylaBusLogic.Manager;
using GameReviewBaylaModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewSite.Controllers
{
    public class UserAccountManagerController : Controller
    {
        private readonly UserAccountManager AccManager;
        private User UserDetails;
        private string uname;
        private string pword;

        public UserAccountManagerController()
        {
            AccManager = new UserAccountManager();
            UserDetails = new User();
            string uname;
        }
        public IActionResult Login()
        {
            AccManager.LoginUser(UserDetails, uname, pword);
            return View();
        }

        public IActionResult Register()
        {
            AccManager.InsertUser(UserDetails);
            return View();
        }

        public IActionResult Show()
        {
            AccManager.RetrieveUser(UserDetails, uname);
            return View();
        }

    }
}
