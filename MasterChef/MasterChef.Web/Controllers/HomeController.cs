namespace MasterChef.Web.Controllers
{
    using System;

    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}

// api/Account/Register -POST [Email Password ConfirmPassword]

// /Token -POST [ grant_type=password,username,password] !NO API/!

// api/Account/Logout -POST

// Add header "Authorization" with value "Bearer [access_token] for [Authorized] access
