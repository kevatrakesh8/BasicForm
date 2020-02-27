using basicForm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace basicForm.Controllers
{
    public class FormHelpersController : Controller
    {
        TestEntities db = new TestEntities();

        // GET: FormHelpers
        
        public ActionResult Open()  // 1 no.
        {
            var CountryData = db.countries.ToList();
            ViewBag.Country = new SelectList(CountryData, "country_id", "country_name");   // only pass the id.
            return View();
        }

        [HttpPost]
        public ActionResult Open(tbl_user tbl)  // 2No.
        {
            db.tbl_user.Add(tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index()   //  3No.
        {
            return View(db.tbl_user.ToList());
        }

        public ActionResult Details(int? id)   //  4 No
        { 
            tbl_user tbl_Usere = db.tbl_user.Find(id);
            return View(tbl_Usere);
        }

       

        public ActionResult Edit(int? id)
        {
            tbl_user tbl_user = db.tbl_user.Find(id);
            ViewBag.user_country_id = new SelectList(db.countries, "country_id", "country_name", tbl_user.user_country_id);
            return View(tbl_user);
        }

        [HttpPost]
        public ActionResult Edit(tbl_user tbl_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.user_country_id = new SelectList(db.countries, "country_id", "country_name", tbl_user.user_country_id);
            return View(tbl_user);
        }

        public ActionResult Delete(int? id)
        {
            tbl_user tbl_user = db.tbl_user.Find(id);
            db.tbl_user.Remove(tbl_user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}