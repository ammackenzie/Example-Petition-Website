using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_Coursework___40091970.Models;

namespace Final_Coursework___40091970.Controllers
{
    public class CauseController : Controller
    {
        // GET: Cause
        CourseworkDB db = new CourseworkDB();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cause()
        {
            return View();
        }
        
        public ActionResult Upload(Cause input)
        {
            var targetEmail = input.CauseOwner.Email;

            Cause toAdd = new Cause
            {
                Title = input.Title,
                Description = input.Description,
                Category = input.Category,
                Action = input.Action
            }; //create new cause from Ajax POST request data

            foreach (Member temp in db.Userbase)
            {
                if(targetEmail == temp.Email)
                {
                    toAdd.CauseOwner = temp; //find registered member based on current user's email address
                    
                }
            }

            db.Causes.Add(toAdd);
            db.SaveChanges();
            return View();

        }

        public ActionResult CauseConfirmation()
        {

            return View(db.Causes.ToList());
        }

        public ActionResult CausePage(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cause cause = db.Causes.Find(id);
            if (cause == null)
            {
                return HttpNotFound(); //in case of bad id pass
            }
            return View(cause);

        }

        public ActionResult SignCause(Cause input)
        {
            
            Signature toAdd = new Signature();

            toAdd.Signer = db.Userbase.SingleOrDefault(m => m.Email == input.CauseOwner.Email); //find registered user and tie them to signature object

            db.Causes.SingleOrDefault(c => c.Id == input.Id).Signatures.Add(toAdd); //add new signature object to target cause
            db.SaveChanges();


            
            return Json("");
        }


        public ActionResult DeleteCause(int id)
        {

            var causeData = db.Causes
                  .Where(c => c.Id == id)
                  .Include(c => c.Signatures)
                  .Include(c => c.CauseOwner); //find cause and record of cause in cause owner's listed causes

            var sigData = db.Signatures
                  .Where(s => s.TheCause.Id == id)
                  .Include(s => s.Signer); //find signature objects associated with cause


            db.Causes.RemoveRange(causeData);
            db.Signatures.RemoveRange(sigData); //remove both data sets from db
            db.SaveChanges();

            return View();
        }
        public ActionResult GetSignatures(int id)
        {
            var signers = db.Causes.SingleOrDefault(c => c.Id == id).Signatures.Select(n => n.Signer.FirstName + " " + n.Signer.LastName).ToList(); //find full name of each signature on cause
      
            return Json(signers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckSigners(int id)
        {
            List<String> signers = db.Causes.SingleOrDefault(c => c.Id == id).Signatures.Select(n => n.Signer.Email).ToList(); //return emails of members who has signed target cause

            return Json(signers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckOwner(int id)
        {

            var owner = db.Causes.SingleOrDefault(c => c.Id == id).CauseOwner.Email;//check if current user is the owner of target cause

            return Json(owner, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CauseList()
        {
            return View(db.Causes.AsEnumerable());
        }
    }
}