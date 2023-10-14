using GameReviewBaylaBusLogic.Manager;
using GameReviewBaylaModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewSite.Controllers
{
    public class UserAccountManagerController : Controller
    {
        private readonly UserAccountManager AccManager;
        private User UserDetails;

        public UserAccountManagerController()
        {
            AccManager = new UserAccountManager();
            UserDetails = new User();
        }
        public IActionResult Login()
        {
            AccManager.LoginUser(UserDetails);
            return View();
        }

        public IActionResult Register()
        {
            AccManager.RegisterUser(UserDetails);
            return View();
        }

        public IActionResult Show()
        {
            AccManager.ShowUser(UserDetails);
            return View();
        }

    }
}
