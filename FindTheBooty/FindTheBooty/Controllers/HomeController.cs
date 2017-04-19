using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindTheBooty.Controllers
{

    public class HomeController : DataController
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

        /// <summary>
        /// Use this Page for testing Back-end Code
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("TestPage")]
        public ActionResult Testing(int? HuntId)
        {
            if (HuntId != null && this.database.treasures.Any(x => x.hunt_hunt_id == HuntId))
            {
                if (!System.IO.File.Exists(Server.MapPath("~/Content/Codes/" + HuntId.ToString())))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Content/Codes/" + HuntId.ToString()));
                }

                IQueryable<FindTheBooty.Models.GeneratedModels.treasure> treasures = this.database.treasures.Where(x => x.hunt_hunt_id == HuntId);
                List<Models.GeneratedModels.treasure> treasureList = new List<Models.GeneratedModels.treasure>();
                string outputPath;
                foreach (Models.GeneratedModels.treasure treasure in treasures)
                {
                    if (!System.IO.File.Exists(Server.MapPath("~/Content/Codes/" + HuntId.ToString() + "/") + HuntId.ToString() + "-" + treasure.hunt_hunt_id + ".png"))
                    {
                        System.Drawing.Bitmap image = QRGenerator.generateQRCode(HuntId.ToString() + "-" + treasure.hunt_hunt_id);
                        outputPath = Server.MapPath("~/Content/Codes/" + HuntId.ToString() + "/") + HuntId.ToString() + "-" + treasure.hunt_hunt_id + ".png";
                        image.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    treasureList.Add(treasure);
                }
                
                if (treasureList.Count > 0)
                {
                    ViewBag.treasureList = treasureList;
                }
            }
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
                string outputPath = Server.MapPath("~/Content/Codes") + "output.png";
                image.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
            }
            
            return RedirectToAction("TestPage");
        }
    }
}