//CreateHuntController
using FindTheBooty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
                    newHunt.hunt_name = model.HuntName.Replace("'", String.Empty);
                    newHunt.hunt_type = "FFA";
                    newHunt.max_users = 150;
                    newHunt.multi_single = 1;
                    newHunt.seq_ffa = "FFA";
                    newHunt.sponsor = null;
                    newHunt.time_create = System.DateTime.Now;
                    newHunt.time_expire = System.DateTime.Now.AddDays(7.0);

                    // Add Hunt to database by adding primary key
                    int latestHunt = database.hunts.OrderBy(h => h.hunt_id).ToList().Last().hunt_id;
                    newHunt.hunt_id = (latestHunt + 1);
                    database.hunts.Add(newHunt);
                    database.SaveChanges();

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
            if (done != null && (bool)done && huntId != null)
            {
                //Redirect to the print page
                return RedirectToAction("PrintPage", new { huntId = huntId });
            }
            if (ModelState.IsValid && huntId != null)
            {
                if (done != null && (bool)done)
                {
                    //Redirect to the print page
                    return RedirectToAction("PrintPage", new { huntId = huntId });
                }
                ViewBag.huntId = huntId;
                //Create the new treasure and set values
                Models.GeneratedModels.treasure newTreasure = new Models.GeneratedModels.treasure();
                newTreasure.description = model.Description;
                newTreasure.hunt_hunt_id = (int)huntId;
                newTreasure.confirmation = "";
                newTreasure.seq_order = 0;
                newTreasure.points = 5;
                //Give the treasure it's unique id
                int latestTreasure = database.treasures.OrderBy(t => t.treasure_id).ToList().Last().treasure_id;
                newTreasure.treasure_id = (latestTreasure + 1);
                //Add and save the treasure to the database
                database.treasures.Add(newTreasure);
                database.SaveChanges();

                //Save treasure image section
                if (Request.Files["treasureImage"].ContentLength > 0)
                {
                    if (!System.IO.File.Exists(Server.MapPath("~/Content/Images/" + huntId.ToString())))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Content/Images/" + huntId.ToString()));
                    }
                    string path1 = string.Format("{0}\\{1}", Server.MapPath("~/Content/Images/" + huntId), (latestTreasure + 1).ToString() + ".png" );
                    if (System.IO.File.Exists(path1))
                        System.IO.File.Delete(path1);

                    Request.Files["treasureImage"].SaveAs(path1);

                }


                //Call the GET AddTreasures to add another treasure
                return RedirectToAction("AddTreasures", new { huntId = huntId });

            }
            else if (huntId == null) //Error handling as in THEORY you shouldn't be able to reach AddTreasures without having seen a HuntId
            {
                return RedirectToAction("AddTreasures");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //QR-Print Section

        [HttpGet]
        public ActionResult PrintPage(int? huntId)
        {
            if (huntId != null && this.database.treasures.Any(x => x.hunt_hunt_id == huntId))
            {
                if (!System.IO.File.Exists(Server.MapPath("~/Content/Codes/" + huntId.ToString())))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Content/Codes/" + huntId.ToString()));
                }

                IQueryable<FindTheBooty.Models.GeneratedModels.treasure> treasures = this.database.treasures.Where(x => x.hunt_hunt_id == huntId);
                List<Models.GeneratedModels.treasure> treasureList = new List<Models.GeneratedModels.treasure>();
                string outputPath;
                foreach (Models.GeneratedModels.treasure treasure in treasures)
                {
                    if (!System.IO.File.Exists(Server.MapPath("~/Content/Codes/" + huntId.ToString() + "/") + huntId.ToString() + "-" + treasure.treasure_id + ".png"))
                    {
                        System.Drawing.Bitmap image = QRGenerator.generateQRCode(huntId.ToString() + "-" + treasure.treasure_id);
                        outputPath = Server.MapPath("~/Content/Codes/" + huntId.ToString() + "/") + huntId.ToString() + "-" + treasure.treasure_id + ".png";
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
    }
}