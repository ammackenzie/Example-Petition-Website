using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Coursework___40091970.Models;

namespace Final_Coursework___40091970.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {

        public CourseworkDB db = new CourseworkDB();

        
        public ActionResult Index()
        {

            //Data data = Data.getInstance(); - was used to initially populate database

            Cause spotlight = db.Causes.SingleOrDefault(Cause => Cause.Title == "End E Corp's Washington Township Plant Emissions");
        

            if(spotlight == null) //if deleted
            {
                Cause backUpSpotlight = db.Causes.First(); //grab first cause in database to spotlight
                if (backUpSpotlight == null)
                {
                    return RedirectToAction("Error"); //if database is empty
                }
                return View(backUpSpotlight);
            }

            return View(spotlight);
        }

    }

}