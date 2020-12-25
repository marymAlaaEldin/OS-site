using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using OSsite.Models;

namespace OSsite.Controllers
{
    public class HomeController : Controller
    {
        private OSSiteDatabaseEntities db = new OSSiteDatabaseEntities();
        public static string txt;

        public ActionResult Index()
        {
            SIGNIN userIN = new SIGNIN();
            userIN.Name = userloggedin.Name;
            userIN.Password = userloggedin.Password;
            return View(userIN);
        }

        /// partial view that show accessories in Home page from database
        [ChildActionOnly]
        public ActionResult RenderAccessories()
        {
            return PartialView(db.Accessories.ToList());
        }
        [ChildActionOnly]
        public ActionResult RenderLaptops()
        {
            return PartialView(db.Labtobs.ToList());
        }
        [ChildActionOnly]
        public ActionResult RenderPhones()
        {
            return PartialView(db.Phones.ToList());
        }
        [ChildActionOnly]
        public ActionResult RenderClothes()
        {
            return PartialView(db.Clothes.ToList());
        }

        /////////////////
        [HttpPost]
        public ActionResult Search(string textbox)
        {
           if (textbox != null)
            {
                var result1 = db.Clothes
                .Where(s => s.name.Contains(textbox) || s.Brand.Contains(textbox));

                var result2 = db.Labtobs
                .Where(s => s.Details.Contains(textbox) || s.model.Contains(textbox) || s.Brand.Contains(textbox));

                var result3 = db.Phones
                    .Where(s => s.Details.Contains(textbox) || s.model.Contains(textbox) || s.Brand.Contains(textbox));

                var result4 = db.Accessories
                    .Where(s => s.Details.Contains(textbox) || s.model.Contains(textbox) || s.Brand.Contains(textbox));

                if (result1.Count() >= result2.Count() && result1.Count() >= result3.Count() && result1.Count() >= result4.Count())
                {
                    txt = textbox;
                    return RedirectToAction("cloSearch", "Home");
                }
                if (result2.Count() >= result1.Count() && result2.Count() >= result3.Count() && result2.Count() >= result4.Count())
                {
                    ViewBag.Message = result2;
                }
                if (result3.Count() >= result2.Count() && result3.Count() >= result1.Count() && result3.Count() >= result4.Count())
                {
                    ViewBag.Message = result3;
                }
                if (result4.Count() >= result2.Count() && result4.Count() >= result3.Count() && result4.Count() >= result1.Count())
                {
                    ViewBag.Message = result4;
                }
                SIGNIN userIN = new SIGNIN();
                userIN.Name = userloggedin.Name;
                userIN.Password = userloggedin.Password;
                //ViewBag.Title = SString.SearchString;
                return View(userIN);
            }
            return RedirectToAction("Index", textbox);
        }
        public ActionResult cloSearch()
        {
            var result1 = db.Clothes
                .Where(s => s.name.Contains(txt) || s.Brand.Contains(txt));
                
            ViewBag.Message = result1;

            SIGNIN userIN = new SIGNIN(); 
            userIN.Name = userloggedin.Name;
            userIN.Password = userloggedin.Password;
            //ViewBag.Title = SString.SearchString;
            return View(userIN);
        }
        /// <summary>
        /// ///////
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "Email,Message")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                contactU.readed = false;
                db.ContactUs.Add(contactU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactU);
        }

    }
}
//@Html.DropDownListFor(m => m.Gender, new List<SelectListItem>
//                   { new SelectListItem{Text="Male", Value="M"},
//                     new SelectListItem{Text="Female", Value="F"}}, "Please select")