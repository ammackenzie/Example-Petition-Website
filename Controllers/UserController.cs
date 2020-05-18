using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_Coursework___40091970.Models;

namespace Final_Coursework___40091970.Controllers
{
    public class UserController : Controller
    {
        private CourseworkDB db = new CourseworkDB();

        // GET: User
        public ActionResult Index()
        {
            return View(db.Userbase.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details()
        {

            return View(db.Userbase.ToList());

        }
        public ActionResult Userbase()
        {
            Dictionary<String, String> userbase = new Dictionary<String, String>();

            foreach (Member tempMember in db.Userbase)
            {
                try
                {
                    userbase.Add(tempMember.Email, tempMember.Password); //add each email and password of current registered users into a dictioonary in key/pair values
            }
                catch (System.ArgumentException e)
            {
                    Console.WriteLine($"The file was not found: '{e}'");
                }

        }


            return Json(userbase, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClientUserbase()
        {
            Dictionary<String, String> clientUserbase = new Dictionary<String, String>();

            foreach (Admin tempAdmin in db.ClientUserbase)
            {
                try
                {
                    clientUserbase.Add(tempAdmin.Email, tempAdmin.Password); //same logic but for admin userbase
                }
                catch (System.ArgumentException e)
                {
                    Console.WriteLine($"The file was not found: '{e}'");
                }

            }

            return Json(clientUserbase, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Register(Member input)
        {
            Member toAdd = new Member
            {
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Password = input.Password
            }; //create new member object with input details

            db.Userbase.Add(toAdd);
            db.SaveChanges();
            return View();
        }
    }
}
