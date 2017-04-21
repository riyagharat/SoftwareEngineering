using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindTheBooty.Models;

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
        public ActionResult SystemEngagement()
        {
            return View();
        }
        public ActionResult TotalPlayTime()
        {
            return View();
        }
        public ActionResult TypesOfHunt()
        {
            return View();
        }
        public ActionResult UpgradeUser()
        {
            // Make check here
            return View();
        }
        [HttpPost]
        public ActionResult UpgradeUser(UpgradeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get list of user with that display_name
                List<Models.GeneratedModels.user> users = database.users.Where(test => test.display_name.Contains(model.AdminInput)).ToList();
                // If number of users is greater then one, give view a list
                if (users.Count > 1)
                {
                    ViewBag.ReturnedUserList = users;
                }
                // Go ahead and promote the user
                else if (users.Count == 1)
                {
                    var userId = users.First().user_id;

                    return RedirectToAction("PromoteUser", new { userId = userId });
                }
            }
            return View();
        }

        public ActionResult PromoteUser(int? userId)
        {
            if (userId != null && database.users.Any(x => x.user_id == userId.ToString()))
            {
                Models.GeneratedModels.user user = database.users.Where(x => x.user_id == userId.ToString()).First();
                user.user_type = "Creator";
                database.SaveChanges();
                ViewBag.user = user.display_name;
                return View();
            }

            return RedirectToAction("UpgradeUser");
        }

    }
}