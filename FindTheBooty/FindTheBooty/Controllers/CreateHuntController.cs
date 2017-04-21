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
                    newHunt.sponsor = null;
                    newHunt.time_create = System.DateTime.Now;
                    newHunt.time_expire = System.DateTime.Now.AddDays(7.0);

                    // Add Hunt to database by adding primary key
                    int latestHunt = database.hunts.OrderBy(h => h.hunt_id.ToString() ?? int.MaxValue.ToString()).ToList().Last().hunt_id;
                    newHunt.hunt_id = (latestHunt + 1);
                    database.hunts.Add(newHunt);
                    database.SaveChanges();
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
                //Get list of treasures with same hunt_hunt_id as the current huntId
                List<Models.GeneratedModels.treasure> bootyList = database.treasures.Where(t => t.hunt_hunt_id == huntId).ToList();
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
                if(done != null && (bool)done)
                {
                    //Redirect to the print page
                }
                ViewBag.huntId = huntId;
                //Create the new treasure and set values
                Models.GeneratedModels.treasure newTreasure = new Models.GeneratedModels.treasure();
                newTreasure.description = model.Description;
                newTreasure.hunt_hunt_id = (int)huntId;
                newTreasure.confirmation = "";
                newTreasure.seq_order = 0;
                newTreasure.points = 5;
                //newTreasure.hunt = database.hunts.
                //Give the treasure it's unique id
                int latestTreasure = database.treasures.OrderBy(t => t.treasure_id).ToList().Last().treasure_id;
                newTreasure.treasure_id = (latestTreasure + 1);
                //Add and save the treasure to the database
                database.treasures.Add(newTreasure);
                database.SaveChanges();

                //Call the GET AddTreasures to add another treasure
                return RedirectToAction("AddTreasures", new { huntId = huntId });

            }
            else if(huntId == null) //Error handling as in THEORY you shouldn't be able to reach AddTreasures without having seen a HuntId
            {
                return RedirectToAction("AddTreasures");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }

}