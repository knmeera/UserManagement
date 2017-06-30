using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Routing.Models.Identity;
using IdentityConsoleApplication.Identity;

namespace Routing.Controllers
{
 
    public class UserController : Controller
    {
        static UserRepository _userRepository = new UserRepository();
     
        public ActionResult Logon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logon(FormCollection f)
        {
                var uname = f["userName"];
                var psw = f["userPassword"];
                long Id = _userRepository.Login(uname, psw);

             

            if (Id != -1)
            {
                User userProfile = _userRepository.GetById(Id);
                Session["UserId"] = Id;
                Session["UserName"] = userProfile.FirstName + " " + userProfile.LastName;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Logon", "User");

        }
       
        public ActionResult Index()
        {
            IEnumerable<User> userlist = _userRepository.GetAll();
            return View(userlist);
        }

        public ActionResult Register()
        {
            ViewData["Message"] = "Please fill all the mandatory fields";
                     return View();

        }
        [HttpPost]
        public ActionResult Register(User usr)
        {
            if (IsValidateForm(usr))
            {
                usr.EmailActive = true;
                usr.IsEnabled = true;

                _userRepository.Add(usr);
                return RedirectToAction("Logon", "User");
            }
            return View();
        }

        public ActionResult TestGIT()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            User user = _userRepository.GetAll().Single(x => x.ID == id);
            return View(user);
        }

        public ActionResult Edit(int id)
        {
            User user = _userRepository.GetAll().Single(x => x.ID == id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,User usr)
        {
            
                if (IsValidateForm(usr))
                {
                    User user = new User();
                    user.ID = usr.ID;
                    user.Email = usr.Email;
                    user.FirstName = usr.FirstName;
                    user.LastName = usr.LastName;
                    user.Mobile = usr.Mobile;
                    user.IsEnabled = usr.IsEnabled;
                    user.EmailActive = usr.EmailActive;
                    user.UserName = usr.UserName;
                    user.Password = usr.Password;
                    _userRepository.Update(user);
                    return RedirectToAction("Index");
                    // TODO: Add update logic here
                }
                return View("Edit");            
        }

        public ActionResult ActivateUser(int id)
        {
            User user = _userRepository.GetAll().Single(x => x.ID == id);
            user.EmailActive = true;
            user.IsEnabled = true;
            return RedirectToAction("Logon", "User");
        }
        private bool IsValidateForm(User user)
        {
            if (user.UserName == string.Empty || user.UserName == null)
            {
                ModelState.AddModelError("UserName", "Please enter Username");
            }
            if (user.LastName == string.Empty || user.LastName == null)
            {
                ModelState.AddModelError("LastName", "Please enter LastName");
            }

            if (user.FirstName == string.Empty || user.FirstName == null)
            {
                ModelState.AddModelError("FirstName", "Please enter FirstName");
            }
            if (user.Password == string.    Empty || user.Password == null)
            {
                ModelState.AddModelError("Password", "Please enter Password");
            }
            if (user.Password != user.Password)
            {
                ModelState.AddModelError("Password", "Password Doesn't match");
            }
            if (user.Email == string.Empty || user.Email == null)
            //if (!string.IsNullOrEmpty(user.Email))
            {
                ModelState.AddModelError("Email", "Please enter Email");
            }
            else
            {
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(user.Email))
                {
                    ModelState.AddModelError("Email", "Email is not valid");
                }
            }
            if (user.Mobile == string.Empty || user.Mobile == null)
            {
                ModelState.AddModelError("Mobile", "Please enter Mobile");
            }
            //if (!string.IsNullOrEmpty(user.Mobile))
            else
            {
                //string mobileRegex = @"/^\d{10}$/";
                string mobileRegex = @"^([0-9]{10})$";
                Regex re = new Regex(mobileRegex);
                if (!re.IsMatch(user.Mobile))
                {
                    ModelState.AddModelError("Mobile", "Mobile is not valid");
                }
            }

            return ModelState.IsValid;
        }
   
    }
}
