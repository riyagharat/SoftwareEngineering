//CreateHuntController
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindTheBooty.Models;

namespace FindTheBooty.Controllers
{
    public class CreateHuntController : DataController
    {

        // GET: HuntSettings
        public ActionResult HuntSettings()
        {
            return View();
        }
        // POST
        [HttpPost]
        public ActionResult HuntSettings(HuntSettingsViewModel model)
        {
            if (ModelState.IsValid)
            { // Inside is where you commit to DB

                if (!this.database.hunts.Any(huntList => huntList.hunt_name == model.HuntName))
                {
                    //Generate the hunt with the information entered by the user
                    Models.GeneratedModels.hunt newHunt = new Models.GeneratedModels.hunt();
                    newHunt.hunt_name = model.HuntName;
                    newHunt.hunt_type = "ffa";
                    newHunt.max_users = 150;
                    newHunt.multi_single = 1;
                    newHunt.seq_ffa = "ffa";
                    newHunt.sponsor = new Models.GeneratedModels.sponsor();
                    newHunt.time_create = System.DateTime.Now;
                    newHunt.time_expire = System.DateTime.Now.AddDays(7.0);

                    // Add Hunt to database by adding primary key
                    int latestHunt = database.hunts.OrderBy(h => h.hunt_id.ToString() ?? int.MaxValue.ToString()).ToList().Last().hunt_id;
                    newHunt.hunt_id = (latestHunt + 1);
                    //database.SaveChanges(); <-- NEEDS TO BE UNIT TESTED

                    return RedirectToAction("AddTreasures", new { huntId = newHunt.hunt_id });
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: AddTreasures
        public ActionResult AddTreasures(int? huntId)
        {
            if (huntId != null)
            {
                IList<Models.GeneratedModels.treasure> bootyList = database.treasures.Where(t => t.hunt_hunt_id == huntId).ToList();
                ViewBag.treasureList = bootyList;
                ViewBag.huntId = huntId;
            }
            else
            {
                return RedirectToAction("HuntSettings");
            }
            return View();
        }
        // POST
        [HttpPost]
        public ActionResult AddTreasures(AddTreasuresViewModel model, int? huntId, Boolean? done) // if done = true, then send to printpage
        {
            if (ModelState.IsValid && huntId != null)
            {
                if(done)
                {
                    //Redirect to the print page
                }
                ViewBag.huntId = huntId;
                Models.GeneratedModels.treasure newTreasure = new Models.GeneratedModels.treasure();
                newTreasure.description = model.Description;
                newTreasure.hunt_hunt_id = (int)huntId;
                newTreasure.confirmation = "false";
                newTreasure.seq_order = 0;
                newTreasure.points = 5;

                int latestTreasure = database.treasures.OrderBy(t => t.treasure_id.ToString() ?? int.MaxValue.ToString()).ToList().Last().treasure_id;
                newTreasure.treasure_id = (latestTreasure + 1);

                return RedirectToAction("AddTreasures", new { huntId = huntId });

            }
            else if(huntId == null)
            {
                return RedirectToAction("AddTreasures");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }

}