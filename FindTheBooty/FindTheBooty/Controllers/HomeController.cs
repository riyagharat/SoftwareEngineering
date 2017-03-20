using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindTheBooty.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // Anything below this line is added for placeholder purposes

        public ActionResult Login()
        {
            ViewBag.Message = "Unnecessary";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult TestPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test()
        {
            HttpPostedFileBase file = Request.Files["inputFile"];
            string str, streamString, output;
            if (file.ContentLength > 0)
            {
                str = file.ToString();
                System.IO.StreamReader reader = new System.IO.StreamReader(file.InputStream);
                streamString = reader.ReadToEnd();
                output = QRReader.getQRCode(file.InputStream);
            }
            return RedirectToAction("TestPage");
        }
    }
}