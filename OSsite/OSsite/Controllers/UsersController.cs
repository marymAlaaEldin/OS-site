using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using OSsite.Models;

namespace OSsite.Controllers
{
    public class UsersController : Controller
    {
        private OSSiteDatabaseEntities db = new OSSiteDatabaseEntities();
        public static string type;
        public static int ID;
        public static Order orderdata = new Order();
        public static int? staticID;
        
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Accessories.ToList());
        }


        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Password,Email,PhoneNumber,ConfirmPassword")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(user);
        }

        //GET: Users/Sgin IN
        public ActionResult SginIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult checkData(SIGNIN data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = db.Users
                    .Where(x => x.Name == data.Name && x.Password == data.Password && x.Email == data.Email)
                    .FirstOrDefault<User>();
                    if (user != null)
                    {
                        userloggedin.ID = user.ID;
                        userloggedin.Name = user.Name;
                        userloggedin.Password = user.Password;
                        userloggedin.phoneNumber = user.PhoneNumber;
                        userloggedin.Email = user.Email;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        data.founded = false;
                    }
                }
                catch
                {
                    data.founded = false;
                    return View("SginIn", data);
                }
            }
            return View("SginIn", data);
        }
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Password,Email,OrderID,CreditCard,Code,baka")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetOrdersFromACC(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var data =db.Accessories.Find(id);
            orderdata.ProductID= data.ProductID;
            orderdata.ProductType= "Accesory";
            return RedirectToAction("GetOrders");
        }
        public ActionResult GetOrdersFromLap(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var data = db.Accessories.Find(id);
            orderdata.ProductID = data.ProductID;
            orderdata.ProductType = "Laptop";
            return RedirectToAction("GetOrders");
        }
        public ActionResult GetOrdersFromclo(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var data = db.Clothes.Find(id);
            orderdata.ProductID = data.ProductID;
            orderdata.ProductType = "Clothes";
            return RedirectToAction("GetOrders");
        }
        public ActionResult GetOrdersFrompho(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var data = db.Phones.Find(id);
            orderdata.ProductID = data.ProductID;
            orderdata.ProductType = "Phone";
            return RedirectToAction("GetOrders");
        }
        //Get Orders
        public ActionResult GetOrders()
        {
            if (userloggedin.Name == null)
                return RedirectToAction("SginIn");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetOrders([Bind(Include = "Email,Phone,Adress")] Order order)
        {
            if(ModelState.IsValid)
            {
                order.ProductType = orderdata.ProductType;
                order.ProductID = orderdata.ProductID;
                order.UserID=userloggedin.ID;
                order.readed = false;
                order.IsValid = false;
                db.Orders.Add(order);
                db.SaveChanges();
                staticID = order.OrderID;
                buildEmail(order.OrderID);
                
                return RedirectToAction("Index", "Home");
               
            }
            return View();
        }


        public ActionResult Confirm(int id)
        {
            var data = db.Orders.Find(ID);

            return View(data);
        }
        public JsonResult ConfirmOrder()
        {
            var data = db.Orders
            .Where(x => x.OrderID == staticID)
            .FirstOrDefault<Order>();

            data.IsValid = true;
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
            staticID = null;

            if (data.ProductType == "Accesory")
            {
                var productData = db.Accessories.Find(data.ProductID);
                ViewBag.name = productData.model;
                ViewBag.price = productData.Price;
            }
            if (data.ProductType == "Laptop")
            {
                var productData = db.Labtobs.Find(data.ProductID);
                ViewBag.name = productData.model;
                ViewBag.price = productData.Price;
            }
            if (data.ProductType == "Clothes")
            {
                var productData = db.Clothes.Find(data.ProductID);
                ViewBag.name = productData.name;
                ViewBag.price = productData.Price;
            }
            if (data.ProductType == "Phone")
            {
                var productData = db.Phones.Find(data.ProductID);
                ViewBag.name = productData.model;
                ViewBag.price = productData.Price;
            }

            var msg = "your Order is confirmed --your order is:   "+ViewBag.name+"  --price: "+ViewBag.price +"  --your Address: "+data.Adress;
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public void buildEmail(int id)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/Users/Email/") + "Text" + ".cshtml");
            var data = db.Orders.Find(id);
            var url = "https://localhost:44362/" + "Users/ConfirmOrder?id" + id;
            body = body.Replace("@ViewBag.confirmationLink", url);
            body = body.ToString();

            buildEmail("confirm order from OS", body, data.Email);
        }

        public static void buildEmail(string subjectTXT, string bodyTXT, string sendTo)
        {
            string from, to, subject, bcc, cc, body;
            from = "osteam141020@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectTXT;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyTXT);
            body = sb.ToString();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add( new MailAddress(to));
            if(!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Timeout = 10000;
            client.Port = 587;
            client.EnableSsl = true;

            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("osteam141020@gmail.com", "Vob63522@");
            try
            {
                client.Send(mail);
            }
            catch(Exception ex)
            {
                string mess = ex.Message;
                throw;
            }
        }
        void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                // Pass the error on to the error page.
                //Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);
            }
        }
        public Order GetOrderData(int id)
        {
            var data = db.Orders.Find(10);
            return data;
        }
        public ActionResult logout()
        {
            userloggedin.ID = 0;
            userloggedin.Name = null;
            userloggedin.Password = null;
            userloggedin.phoneNumber = null;
            userloggedin.Email = null;

            return RedirectToAction("Index", "Home");
        }
    }
}
