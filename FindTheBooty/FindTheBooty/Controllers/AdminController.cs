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
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;

            SqlConnection piratecharts = new SqlConnection(ConfigurationManager.ConnectionStrings["FTBConnection"].ToString());
            piratecharts.Open();

            SqlCommand query1 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Land Lubber';", piratecharts);
            num1 = Convert.ToInt32(query1.ExecuteScalar());

            SqlCommand query2 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Scallywag';", piratecharts);
            num2 = Convert.ToInt32(query2.ExecuteScalar());

            SqlCommand query3 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Deck Hand';", piratecharts);
            num3 = Convert.ToInt32(query3.ExecuteScalar());

            SqlCommand query4 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Buccaneer';", piratecharts);
            num4 = Convert.ToInt32(query4.ExecuteScalar());

            SqlCommand query5 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Quartermaster';", piratecharts);
            num5 = Convert.ToInt32(query5.ExecuteScalar());

            SqlCommand query6 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Captain';", piratecharts);
            num6 = Convert.ToInt32(query6.ExecuteScalar());

            SqlCommand query7 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Pirate Supreme';", piratecharts);
            num7 = Convert.ToInt32(query7.ExecuteScalar());

            SqlCommand query8 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Doug Leas';", piratecharts);
            num8 = Convert.ToInt32(query8.ExecuteScalar());

            SqlCommand query9 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Sea Legs';", piratecharts);
            num9 = Convert.ToInt32(query9.ExecuteScalar());

            SqlCommand query10 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Fortune Finder';", piratecharts);
            num10 = Convert.ToInt32(query10.ExecuteScalar());

            SqlCommand query11 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Master Plunderer';", piratecharts);
            num11 = Convert.ToInt32(query11.ExecuteScalar());

            SqlCommand query12 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'So This Is Booty';", piratecharts);
            num12 = Convert.ToInt32(query12.ExecuteScalar());

            SqlCommand query13 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Live For the Booty';", piratecharts);
            num13 = Convert.ToInt32(query13.ExecuteScalar());

            SqlCommand query14 = new SqlCommand("SELECT COUNT(*) FROM dbo.[user] WHERE rank = 'Sic Parvis Magna';", piratecharts);
            num14 = Convert.ToInt32(query14.ExecuteScalar());

            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            .SetXAxis(new XAxis
            {
                Categories = new[] {"Land Lubber", "Scallywag", "Deck Hand", "Buccaneer", "Quartermaster", "Captain", "Pirate Supreme", "Doug Leas", "Sea Legs",
                "Fortune Finder", "Master Plunderer", "So This Is Booty", "Live For the Booty", "Sic Parvis Magna"}
            })
            .SetSeries(new Series
            {   Name = "Pirate Type",
                Data = new Data(new object[] { num1, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11, num12, num13, num14 })
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