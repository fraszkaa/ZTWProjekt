using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTW.Models;

namespace ZTW.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users U)
        {
            if (ModelState.IsValid)
            {
                using (ZTWDatabaseEntities2 dc = new ZTWDatabaseEntities2())
                {
                    //you should check duplicate registration here 
                    dc.Users.Add(U);
                    dc.SaveChanges();
                    ModelState.Clear();
                    U = null;
                    ViewBag.Message = "Successfully Registration Done";
                }
            }
            return View(U);
        }

      
        public ActionResult Login()
        {
            Console.WriteLine("Login");
            System.Diagnostics.Debug.Write("Login");
            return View();

        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users u)
        {

            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("FullName");

            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (ZTWDatabaseEntities2 dc = new ZTWDatabaseEntities2())
                {
                    Console.WriteLine("Zalogowano");
                    var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.UPassword.Equals(u.UPassword)).FirstOrDefault();
                
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.idUser.ToString();
                        Session["LogedUserFullname"] = v.FullName.ToString();
                        if (v.UserName.ToString() == "admin")
                        {
                            return RedirectToAction("AfterLoginAdmin");
                        }
                         return RedirectToAction("AfterLogin");
                    }
                }
            }
            return View(u);
        }

        public ActionResult Login2()
        {
            Console.WriteLine("Login");
            System.Diagnostics.Debug.Write("Login");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login2(Users u)
        {
            Console.WriteLine("Post");
            // this action is for handle post (login)
                if (ModelState.IsValid) // this is check validity
                {
                    using (ZTWDatabaseEntities2 dc = new ZTWDatabaseEntities2())
                    {
                        Console.WriteLine("Zalogowano");
                        var v = dc.Users.Where(a => a.UserName.Equals(u.UserName) && a.UPassword.Equals(u.UPassword)).FirstOrDefault();
                        if (v != null)
                        {
                            Session["LogedUserID"] = v.idUser.ToString();
                            Session["LogedUserFullname"] = v.FullName.ToString();
                            return RedirectToAction("AfterLogin");
                        }
                    }
                }

        
        /*
            Session["LogedUserID"] = "1";
            Session["LogedUserFullname"] = "user1";
            Session["LogeUPassword"] = "12345678";

            return RedirectToAction("AfterLogin");

    */
            return View(u);
        }


        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult AfterLoginAdmin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}