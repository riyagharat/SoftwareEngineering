using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindTheBooty.Models;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;

namespace FindTheBooty.Controllers
{
    /**
     *  So // and /* are regular comments. But /// will generate an XML that we can use later for docs.
     *  It's a good practice to keep. I'll make my notes in regular comments.
     *  
     *  This AdminController class will have reference to all Views in the Views/Admin folder
     */
    public class AdminController : DataController
    {
        /// <summary>
        /// Not the controller action that Riya needs. But the controller action Riya deserves.
        /// </summary>
        /// <returns>Admin.cshtml View</returns>
        // This "Action" is called Admin and returns an ActionResult. The ActionResult is the
        // view Admin.cshtml because the function is called Admin.
        // Additional Views in the View/Admin folder will need similar named .cshtml files and
        // functions in this controller.
        public ActionResult Admin()
        {
            // Remember the Viewbag is dynamic content that I can use in the View.
            System.Random randomness = new System.Random(System.Environment.TickCount);
            ViewBag.RandomNumber = randomness.Next(0, 26);
            ViewBag.RandomCharacter = (char)('A' + ViewBag.RandomNumber);
            return View();
        }

        public ActionResult BulkImport()
        {
            return View();
        }
        public ActionResult BulkExport()
        {
            return View();
        }
        public ActionResult TreasuresFound()
        {
            return View();
        }
        public ActionResult PirateDistribution()
        {
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
        .SetXAxis(new XAxis
        {
            Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
        })
        .SetSeries(new Series
        {
            Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
        });

            return View(chart);
        }
        public ActionResult TypesOfUsers()
        {
            return View();
        }
        public ActionResult UpgradeUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpgradeUser(UpgradeUserViewModel model)
        {
            var usersList = new Models.GeneratedModels.user();
            if (ModelState.IsValid)
            {
               usersList = database.users.Where(test => test.display_name == model.AdminInput).ToList().First();
            }
            return View();
        }

    }
}