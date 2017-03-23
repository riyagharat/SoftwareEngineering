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

        /// <summary>
        /// Use this Page for testing Back-end Code
        /// </summary>
        /// <returns></returns>
        public ActionResult TestPage()
        {
            return View();
        }

        /// <summary>
        /// Use this page for testing the responses to front-end code
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Test(string inputString)
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
            else if (inputString.Length > 0)
            {
                System.Drawing.Bitmap image = QRGenerator.generateQRCode(inputString);
                string outputPath = Server.MapPath("~/") + "output.png";
                image.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
            }
            
            return RedirectToAction("TestPage");
        }
    }
}