using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OSsite.Models;

namespace OSsite.Controllers
{
    public class AdminsController : Controller
    {
        private OSSiteDatabaseEntities db = new OSSiteDatabaseEntities();
        public LoginAdmin Admon = new LoginAdmin();
        // GET: Admins
        public ActionResult DashBoard()
        {
            if (Adminloggedin.password == null)
                return RedirectToAction("LogIN");

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(db.Admins.ToList());
        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult UsersData()
        {
            if (Adminloggedin.password == null)
                return RedirectToAction("LogIN");
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(db.Users.ToList());
        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult ContactUsMessages()
        {
            if (Adminloggedin.password == null)
                return RedirectToAction("LogIN");
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(db.ContactUs.ToList());
        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult MessageDetails(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactU message = db.ContactUs.Find(ID);
            
            if (message == null)
            {
                return HttpNotFound();
            }
            message.readed = true;
            db.Entry(message).State = EntityState.Modified;
            db.SaveChanges();

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(message);
        }
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult ProductsData()
        {
            if (Adminloggedin.password == null)
                return RedirectToAction("LogIN");
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(db.Labtobs.ToList());
        }
        [ChildActionOnly]
        public ActionResult RenderAccessories()
        {
            return PartialView(db.Accessories.ToList());
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
        /// <summary>
        /// orders
        /// </summary>
        /// <returns></returns>
        public ActionResult OrdersData()
        {
            if (Adminloggedin.password == null)
                return RedirectToAction("LogIN");
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(db.Orders.ToList());
        }
        public ActionResult OrderDetails(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(ID);

            if (order == null)
            {
                return HttpNotFound();
            }
            order.readed = true;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(order);
        }
        public ActionResult AddLaptop()
        {
            if (Adminloggedin.password == null)
                return RedirectToAction("LogIN");

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLaptop(Laptop laptop)
        {
            if(ModelState.IsValid)
            {
                Labtob labtopData = new Labtob();
                labtopData.img = new byte[laptop.file.ContentLength];
                MemoryStream target = new MemoryStream();
                laptop.file.InputStream.CopyTo(target);
                labtopData.img = target.ToArray();
                labtopData.model = laptop.model;
                labtopData.Details = laptop.Details;
                labtopData.Brand = laptop.Brand;
                labtopData.Price = laptop.Price;
                labtopData.sale = laptop.sale;
                labtopData.PicesNO = laptop.PicesNO;

                db.Labtobs.Add(labtopData);
                db.SaveChanges();

                return RedirectToAction("DashBoard");
            }

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(laptop);
        }
        public ActionResult AddPhone()
        {
            if (Adminloggedin.password == null)
                return RedirectToAction("LogIN");

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPhone(AddPhone addphone)
        {
            if (ModelState.IsValid)
            {
                Phone phoneData = new Phone();
                phoneData.img = new byte[addphone.file.ContentLength];
                MemoryStream target = new MemoryStream();
                addphone.file.InputStream.CopyTo(target);
                phoneData.img = target.ToArray();
                phoneData.model = addphone.model;
                phoneData.Details = addphone.Details;
                phoneData.Brand = addphone.Brand;
                phoneData.Price = addphone.Price;
                phoneData.sale = addphone.sale;
                phoneData.PicesNo = addphone.PicesNO;

                db.Phones.Add(phoneData);
                db.SaveChanges();

                return RedirectToAction("DashBoard");
            }

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(addphone);
        }
        public ActionResult AddAccessory()
        {
            if (Adminloggedin.password == null)
                return RedirectToAction("LogIN");

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAccessory(AddAccessory addAccessory)
        {
            if (ModelState.IsValid)
            {
                Accessory AccData = new Accessory();
                AccData.img = new byte[addAccessory.file.ContentLength];
                MemoryStream target = new MemoryStream();
                addAccessory.file.InputStream.CopyTo(target);
                AccData.img = target.ToArray();
                AccData.model = addAccessory.model;
                AccData.Details = addAccessory.Details;
                AccData.Brand = addAccessory.Brand;
                AccData.Price = addAccessory.Price;
                AccData.sale = addAccessory.sale;
                AccData.PicesNO = addAccessory.PicesNO;

                db.Accessories.Add(AccData);
                db.SaveChanges();

                return RedirectToAction("DashBoard");
            }

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(addAccessory);
        }
        public ActionResult AddClothes()
        {
            if (Adminloggedin.password == null)
                return RedirectToAction("LogIN");

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClothes(AddClothes addClothes)
        {
            if (ModelState.IsValid)
            {
                Cloth clothesData = new Cloth();
                clothesData.img = new byte[addClothes.file.ContentLength];
                MemoryStream target = new MemoryStream();
                addClothes.file.InputStream.CopyTo(target);
                clothesData.img = target.ToArray();
                clothesData.name = addClothes.name;
                clothesData.Size = addClothes.Size;
                clothesData.Brand = addClothes.Brand;
                clothesData.Price = addClothes.Price;
                clothesData.sale = addClothes.sale;
                clothesData.color = addClothes.color;

                db.Clothes.Add(clothesData);
                db.SaveChanges();

                return RedirectToAction("DashBoard");
            }

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(addClothes);
        }
        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (Adminloggedin.password == null)
                return RedirectToAction("LogIN");

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.Type = "normal";
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("DashBoard");
            }

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(admin);
        }
        public ActionResult LogIN()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIN(LoginAdmin loginAdmin)
        {
            if(ModelState.IsValid)
            {
                var data = db.Admins
                .Where(x => x.name == loginAdmin.name && x.password == loginAdmin.password)
                .FirstOrDefault<Admin>();
                if (data != null)
                {
                    Adminloggedin.name = data.name;
                    Adminloggedin.password = data.password;
                    Adminloggedin.type = data.Type;

                    return RedirectToAction("DashBoard");
                }
                else
                {
                    ViewBag.message = "not found";
                }
                
            }
            return View();
        }

        // GET: Admins/Delete/5
        public ActionResult DeleteClothes(int? id)
        {
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth data = db.Clothes.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(data);
        }
        
        [HttpPost, ActionName("DeleteClothes")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteClothesConfirmed(int id)
        {
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Cloth data = db.Clothes.Find(id);
            db.Clothes.Remove(data);
            db.SaveChanges();
            return RedirectToAction("DashBoard");
        }

        /// <summary>
        /// phoneee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeletePhone(int? id)
        {
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            if (id == null)
            {
                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone data = db.Phones.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(data);
        }
        [HttpPost, ActionName("DeletePhone")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhoneConfirmed(int id)
        {
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Phone data = db.Phones.Find(id);
            db.Phones.Remove(data);
            db.SaveChanges();
            return RedirectToAction("DashBoard");
        }
        /// <summary>
        /// //delet laptop
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult DeleteLaptop(int? id)
        {
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labtob data = db.Labtobs.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(data);
        }
        [HttpPost, ActionName("DeleteLaptop")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLaptopConfirmed(int id)
        {
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Labtob data = db.Labtobs.Find(id);
            db.Labtobs.Remove(data);
            db.SaveChanges();
            return RedirectToAction("DashBoard");
        }

        /// <summary>
        /// /delet acc
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult DeleteAccessory(int? id)
        {
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessory data = db.Accessories.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(data);
        }
        [HttpPost, ActionName("DeleteAccessory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccessoryConfirmed(int id)
        {
            //Force refreshing browser
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Accessory data = db.Accessories.Find(id);
            db.Accessories.Remove(data);
            db.SaveChanges();
            return RedirectToAction("DashBoard");
        }

        public ActionResult Logout()
        {
            Adminloggedin.name = null;
            Adminloggedin.password = null;
            Adminloggedin.type = null;

            return RedirectToAction("DashBoard");
        }


        //Get:laptop/Edite
        public ActionResult EditLaptop(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labtob data = db.Labtobs.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            Laptop PassedData = new Laptop();
            PassedData.ID = data.ProductID;
            PassedData.model = data.model;
            PassedData.Details = data.Details;
            PassedData.Brand = data.Brand;
            PassedData.Price = data.Price;
            PassedData.sale = data.sale;
            PassedData.PicesNO = data.PicesNO;
            PassedData.img = data.img;
            

            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(PassedData);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLaptop( Laptop data)
        {
            if (ModelState.IsValid)
            {
                var PassedData = db.Labtobs.Find(data.ID);
                PassedData.ProductID = data.ID;
                PassedData.model = data.model;
                PassedData.Details = data.Details;
                PassedData.Brand = data.Brand;
                PassedData.Price = data.Price;
                PassedData.sale = data.sale;
                PassedData.PicesNO = data.PicesNO;
                MemoryStream target = new MemoryStream();
                data.file.InputStream.CopyTo(target);
                PassedData.img = target.ToArray();


                db.Entry(PassedData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DashBoard");
            }
            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;
            return View(data);
        }
       /////////////////////////////edit Acc
        public ActionResult EditAcc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessory data = db.Accessories.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            AddAccessory PassedData = new AddAccessory();
            PassedData.ID = data.ProductID;
            PassedData.model = data.model;
            PassedData.Details = data.Details;
            PassedData.Brand = data.Brand;
            PassedData.Price = data.Price;
            PassedData.sale = data.sale;
            PassedData.PicesNO = data.PicesNO;
            PassedData.img = data.img;


            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(PassedData);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAcc(AddAccessory data)
        {
            if (ModelState.IsValid)
            {
                var PassedData = db.Accessories.Find(data.ID);
                PassedData.ProductID = data.ID;
                PassedData.model = data.model;
                PassedData.Details = data.Details;
                PassedData.Brand = data.Brand;
                PassedData.Price = data.Price;
                PassedData.sale = data.sale;
                PassedData.PicesNO = data.PicesNO;
                MemoryStream target = new MemoryStream();
                data.file.InputStream.CopyTo(target);
                PassedData.img = target.ToArray();


                db.Entry(PassedData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DashBoard");
            }
            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;
            return View(data);
        }

        /////////////////////////////edit clothes
        public ActionResult EditClothes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth data = db.Clothes.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            AddClothes PassedData = new AddClothes();
            PassedData.productID = data.ProductID;
            PassedData.name = data.name;
            PassedData.color = data.color;
            PassedData.Brand = data.Brand;
            PassedData.Price = data.Price;
            PassedData.sale = data.sale;
            PassedData.Size = data.Size;
            PassedData.img = data.img;


            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(PassedData);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClothes(AddClothes data)
        {
            
            if (ModelState.IsValid)
            {
                var PassedData = db.Clothes.Find(data.productID);
                PassedData.ProductID = data.productID;
                PassedData.name = data.name;
                PassedData.color = data.color;
                PassedData.Brand = data.Brand;
                PassedData.Price = data.Price;
                PassedData.sale = data.sale;
                PassedData.Size = data.Size;
                MemoryStream target = new MemoryStream();
                data.file.InputStream.CopyTo(target);
                PassedData.img = target.ToArray();


                db.Entry(PassedData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DashBoard");
            }
            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;
            return View(data);
        }

        public ActionResult EditPhone(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone data = db.Phones.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            AddPhone PassedData = new AddPhone();
            PassedData.ID = data.ProductID;
            PassedData.model = data.model;
            PassedData.Details = data.Details;
            PassedData.Brand = data.Brand;
            PassedData.Price = data.Price;
            PassedData.sale = data.sale;
            PassedData.PicesNO = data.PicesNo;
            PassedData.img = data.img;


            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;

            return View(PassedData);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPhone(AddPhone data)
        {
            if (ModelState.IsValid)
            {
                var PassedData = db.Phones.Find(data.ID);
                PassedData.ProductID = data.ID;
                PassedData.model = data.model;
                PassedData.Details = data.Details;
                PassedData.Brand = data.Brand;
                PassedData.Price = data.Price;
                PassedData.sale = data.sale;
                PassedData.PicesNo = data.PicesNO;
                MemoryStream target = new MemoryStream();
                data.file.InputStream.CopyTo(target);
                PassedData.img = target.ToArray();


                db.Entry(PassedData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DashBoard");
            }
            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;
            return View(data);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin Admin = db.Admins.Find(id);
            if (Admin == null)
            {
                return HttpNotFound();
            }
            Admon.name = Adminloggedin.name;
            Admon.password = Adminloggedin.password;
            Admon.Type = Adminloggedin.type;
            ViewBag.Message = Admon;
            return View(Admin);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("DashBoard");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
