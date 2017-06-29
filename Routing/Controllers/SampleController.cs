using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using IdentityConsoleApplication.Identity;
using Routing.Models;

namespace Routing.Controllers
{
    public class SampleController : Controller
    {
        static UserRepository _userRepository = new UserRepository();
        //
        // GET: /Sample/

        public ViewResult Test()
        {
            return View();
        }
       

        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(TestUser model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(model.Email))
                {
                    ModelState.AddModelError("Email", "Email is not valid");
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Email is required");
            }
            if (ModelState.IsValid)
            {
                ViewBag.Name = model.Name;
                ViewBag.Email = model.Email;
            }
            return View(model);
        }

        //
        // GET: /Sample/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Sample/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Sample/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Sample/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Sample/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Sample/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Sample/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
