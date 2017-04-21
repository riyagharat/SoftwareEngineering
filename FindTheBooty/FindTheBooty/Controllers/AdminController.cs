using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindTheBooty.Models;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;
using System.Data.SqlClient;
using System.Configuration;

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
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            .SetXAxis(new XAxis { Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" } })
            .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Sales" } })
            .SetSeries(new Series { Data = new Data(new object[] { 20, 30, 40, 50, 20, 60, 14, 72, 30, 35, 10, 20 }), Name = "Sales" })
            .SetTitle(new Title { Text = "Sales Data" })
            .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column });
            return View(chart);
        }
        public ActionResult PirateDistribution()
        {
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            .SetXAxis(new XAxis
            {
            Categories = new[] {"Land Lubber", "Scallywag", "Deck Hand", "Buccaneer", "Quartermaster", "Captain", "Pirate Supreme", "Doug Leas", "Sea Legs", "Fortune Finder", "Master Plunderer",
                    "So This Is Booty", "Live For the Booty", "Sic Parvis Magna"}
            })
            .SetSeries(new Series
            {
            Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
            })
            .SetTitle(new Title { Text = "Pirate Distribution" });

            return View(chart);
        }
        public ActionResult TypesOfUsers()
        {
            int numberAdmins = 0;
            int numberUsers = 0;
            int numberCreators = 0;

            SqlConnection usercharts = new SqlConnection(ConfigurationManager.ConnectionStrings["FTBConnection"].ToString());
            usercharts.Open();

            SqlCommand queryAdmin = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE user_type = 'Admin';", usercharts);
            numberAdmins = Convert.ToInt32(queryAdmin.ExecuteScalar());

            SqlCommand queryCreator = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE user_type = 'Creator';", usercharts);
            numberCreators = Convert.ToInt32(queryCreator.ExecuteScalar());

            SqlCommand queryUser = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE user_type = 'User';", usercharts);
            numberUsers = Convert.ToInt32(queryUser.ExecuteScalar());


            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            .SetSeries(new Series
            {
                Type = ChartTypes.Pie,
                Name = "Number of User",
                Data = new Data(new object[] { new object[] {"Admin", numberAdmins},
                                               new object[] {"Creator", numberCreators},
                                               new object[] {"User", numberUsers } })

            })
            .SetTitle(new Title
            {
                Text = "Types of User"
            });

            return View(chart);
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