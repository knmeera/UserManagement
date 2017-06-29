using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityConsoleApplication.Identity;


namespace Routing.Controllers
{
    public class HomeController : Controller
    {
        static UserRepository _userRepository = new UserRepository();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.users = new SelectList(_userRepository.GetAll(), "Id", "UserName");

            return View();
        }


        public ActionResult UserList()
        {
            return PartialView("_userList", _userRepository.GetAll());
        }

        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
    }
}
