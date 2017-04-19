using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindTheBooty.Controllers
{
    public class ParticipatingController : DataController
    {

        // GET: Participating
        public ActionResult Index()
        {
            return RedirectToAction("JoinedHunts");
        }

        // GET: Participating/JoinedHunts
        public ActionResult JoinedHunts(bool error = false)
        {

            // build model for JoinedHunts view
            Models.JoinedHuntList joined = new Models.JoinedHuntList();
            if (error)
                joined.DoHuntError = true;
            joined.HuntList = this.GetJoinedHunts( (Models.GeneratedModels.user)Session["LoggedUser"] );

            return View(joined);
        }

        // Poll DB for hunts joined and participating
        public List<Models.Hunt> GetJoinedHunts(Models.GeneratedModels.user session = null)
        {
            List<Models.Hunt> HuntList = new List<Models.Hunt>();
            List<Models.GeneratedModels.hunt> tmpHuntList = new List<Models.GeneratedModels.hunt>();

            // if no session was passed, return empty list
            if (session == null)
                return HuntList;

            // snag user hunt relation for logged in user
            List<Models.GeneratedModels.user_hunt_relation> joinedHuntRelation = database.user_hunt_relation.Where(u => u.user_user_id == session.user_id)
                .OrderBy(h => h.hunt_hunt_id)
                .ToList();

            // pull hunt data for each joined hunt
            foreach (var huntRelation in joinedHuntRelation)
            {
                var tmpHuntId = huntRelation.hunt_hunt_id;
                tmpHuntList.Add(
                    database.hunts.Where(h => h.hunt_id == tmpHuntId)
                        .First()
                );
            }

            // build Hunt list using Participating model
            foreach(var hunt in tmpHuntList)
            {
                Models.Hunt tmpHunt = new Models.Hunt();
                tmpHunt.HuntID = hunt.hunt_id;
                tmpHunt.HuntName = hunt.hunt_name;
                tmpHunt.HuntType = hunt.hunt_type;
                tmpHunt.MaxNumOfUsers = hunt.max_users;
                //tmpHunt.MultiOrSingle = hunt.multi_single;
                tmpHunt.SponsorID = Convert.ToInt64(hunt.sponsor_sponsor_id);
                tmpHunt.TimeCreate = hunt.time_create;
                tmpHunt.TimeExpire = hunt.time_expire;
                tmpHunt.SeqOrFFA = hunt.seq_ffa;
                HuntList.Add(tmpHunt);
            }

            // return list of hunts the user has joined
            return HuntList;

        }

        // GET: Participating/JoinableHunts
        public ActionResult JoinableHunts(bool error = false)
        {
            Models.JoinableHuntList joinable = new Models.JoinableHuntList();
            if (error)
                joinable.JoinHuntError = true;
            joinable.HuntList = this.GetJoinableHunts( (Models.GeneratedModels.user)Session["LoggedUser"] );
            return View(joinable);
        }

        // Get list of Joinable hunts
        public List<Models.Hunt> GetJoinableHunts(Models.GeneratedModels.user session = null)
        {
            List<Models.Hunt> huntList = new List<Models.Hunt>();
            List<Models.GeneratedModels.hunt> joinableHuntList = new List<Models.GeneratedModels.hunt>();

            // if user is not logged in, return empty list
            if (session == null)
                return huntList;
            
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FTBConnection"].ConnectionString;
            
            string query = "SELECT * " +
            "FROM dbo.hunt as h " +
            "LEFT JOIN dbo.user_hunt_relation as r " +
            "ON r.hunt_hunt_id = h.hunt_id " +
            "WHERE r.user_user_id IS NULL OR r.user_user_id != @Id;";

            // open db connection and build list of hunts
            using (System.Data.SqlClient.SqlConnection db = new System.Data.SqlClient.SqlConnection(connectionString))
            using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(query, db))
            {
                db.Open();
                command.Parameters.AddWithValue("@Id", session.user_id);
                System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();

                // loop and build huntList for joinable hunts
                while (reader.Read())
                {
                    FindTheBooty.Models.Hunt hunt = new FindTheBooty.Models.Hunt();
                    hunt.HuntID = System.Convert.ToInt32(reader["hunt_id"]);
                    hunt.HuntName = reader["hunt_name"].ToString();
                    hunt.HuntType = reader["hunt_type"].ToString();
                    hunt.TimeExpire = System.Convert.ToDateTime(reader["time_expire"].ToString());


                    Models.Hunt tmpHunt = new Models.Hunt();
                    tmpHunt.HuntID = Convert.ToInt64(reader["hunt_id"]);
                    tmpHunt.HuntName = (string)reader["hunt_name"];
                    tmpHunt.HuntType = (string)reader["hunt_type"];
                    tmpHunt.MaxNumOfUsers = (int)reader["max_users"];
                    //tmpHunt.MultiOrSingle = hunt.multi_single;
                    tmpHunt.SponsorID = reader["sponsor_sponsor_id"] as long? ?? -1;
                    tmpHunt.TimeCreate = (DateTime) reader["time_create"];
                    tmpHunt.TimeExpire = (DateTime) reader["time_expire"];
                    tmpHunt.SeqOrFFA = (string)reader["seq_ffa"];
                    huntList.Add(tmpHunt);
                }
            }

            return huntList;

        }

        // GET: Participating/JoinHunt/{id of hunt to join}
        public ActionResult JoinHunt(int id = -1)
        {
            Models.GeneratedModels.user userSession = (Models.GeneratedModels.user)Session["LoggedUser"];
            Models.GeneratedModels.hunt hunt;

            // check if id is invalid, user is not logged in or the hunt id doesn't exist
            if (id < 0 
                || Session["LoggedUser"] == null 
                || database.hunts.Where(h => h.hunt_id == id).Count() <= 0 
                || database.user_hunt_relation.Where(u => u.hunt_hunt_id == id 
                    && u.user_user_id == userSession.user_id).Count() > 0
                )
            {
                return RedirectToAction("JoinableHunts", new { error = true});
            }
            else
            {
                // add relation in table
                Models.GeneratedModels.user_hunt_relation relation = new Models.GeneratedModels.user_hunt_relation();

                relation.hunt_hunt_id = id;
                relation.user_user_id = userSession.user_id;
                relation.completed = Convert.ToString(false);
                relation.active = Convert.ToString(true);

                database.user_hunt_relation.Add(relation);

                // lookup all treasures and build relations
                List<Models.GeneratedModels.treasure> huntTreasures = database.treasures.Where(t => t.hunt_hunt_id == id).ToList();
                foreach(Models.GeneratedModels.treasure treasure in huntTreasures)
                {
                    Models.GeneratedModels.user_treasure_relation tmpRelation = new Models.GeneratedModels.user_treasure_relation();
                    tmpRelation.user_user_id = userSession.user_id;
                    tmpRelation.treasure_hunt_hunt_id = id;
                    tmpRelation.treasure_treasure_id = treasure.treasure_id;
                    tmpRelation.found = false.ToString();
                    database.user_treasure_relation.Add(tmpRelation);
                }

                // commit relation changes to the db
                database.SaveChanges();

                hunt = database.hunts.Where(h => h.hunt_id == id).First();
            }
            return View(hunt);
        }

        // GET: Participating/DoHunt{id of hunt to continue in}
        public ActionResult DoHunt(int id = -1)
        {
            // check if hunt ID is in a valid format
            if (id < 0)
            {
                return RedirectToAction("JoinedHunts", new { error = true });
            }

            // check if hunt exists
            Models.GeneratedModels.hunt hunt = database.hunts.Where(h => h.hunt_id == id).First();
            if (hunt == null)
            {
                return RedirectToAction("JoinedHunts", new { error = true });
            }

            // initialize variables and grab/assign hunt, treasure and treasure relation data
            Models.GeneratedModels.user session = (Models.GeneratedModels.user)Session["LoggedUser"];
            List<Models.Treasure> actualTreasureList = new List<Models.Treasure>();
            double total = 0;
            double found = 0;

            // build lists of treasure info and user relation to those treasures
            List<Models.GeneratedModels.treasure> treasureDetailList = database.treasures.Where(r => r.hunt_hunt_id == id).ToList();
            List<Models.GeneratedModels.user_treasure_relation> treasureRelationList = database.user_treasure_relation
                .Where(r => r.treasure_hunt_hunt_id == id && r.user_user_id == session.user_id).ToList();


            // build treasure models for each treasure
            foreach(Models.GeneratedModels.treasure treasure in treasureDetailList)
            {
                ++total;
                Models.Treasure tmpTreasure = new Models.Treasure();
                tmpTreasure.Id = treasure.treasure_id;
                tmpTreasure.Points = treasure.points;
                tmpTreasure.Description = treasure.description;
                tmpTreasure.Found = Convert.ToBoolean(treasureRelationList.Where(r => r.treasure_treasure_id == treasure.treasure_id).First().found);
                if(tmpTreasure.Found == true)
                    ++found;

                // assign creator uploaded image file, if exists, otherwise placeholder image
                if (System.IO.File.Exists("/Content/Images/" + treasure.hunt_hunt_id + "/" + treasure.treasure_id + ".jpg"))
                {
                    tmpTreasure.Image = "/Content/Images/" + treasure.hunt_hunt_id + "/" + treasure.treasure_id + ".jpg";
                }
                else
                {
                    tmpTreasure.Image = "/Content/Images/booty-not-found.png";
                }
                actualTreasureList.Add(tmpTreasure);
            }

            // build ViewModel
            Models.DoHuntList doHuntList = new Models.DoHuntList();
            doHuntList.Hunt = hunt; // assign associated hunt info
            doHuntList.TreasureList = actualTreasureList;
            if (total == 0)
                doHuntList.PercentageComplete = 0;
            else
                doHuntList.PercentageComplete = (int)((found / total) * 100);

            return View(doHuntList);
        }

        // POST: Participating/UploadQR
        [HttpPost]
        public ActionResult UploadQR()
        {
            // initialize response object
            /* success value description:
                -2 -- No relation found --> user not participating in this hunt
                -1 -- Invalid format/QR Code not found
                0  -- Image failed to upload
                1  -- Upload/processing success
            */
            Models.GeneratedModels.user session = (Models.GeneratedModels.user)Session["LoggedUser"];
            var response = new Dictionary<string, object>();
            response.Add("huntId", -1);
            response.Add("treasureId", -1);
            response.Add("success", 0);

            // capture uploaded QR Code image
            HttpPostedFileBase qrImage = Request.Files["qr-image"];

            // validate file was submitted
            if (qrImage != null && qrImage.ContentLength > 0)
            {
                string[] QRValueSplit;
                string QRHuntId;
                string QRTreasureId;

                var QRValue = QRReader.getQRCode(qrImage.InputStream);

                // validate we have the correct format
                if (QRValue == null || !QRValue.Contains("-"))
                {
                    response["success"] = -1; // status = failure
                    return Json(response);
                }

                QRValueSplit = QRValue.Split('-');
                QRHuntId = QRValueSplit[0];
                QRTreasureId = QRValueSplit[1];
                long QRHuntIdLong = Convert.ToInt64(QRHuntId);
                long QRTreasureIdLong = Convert.ToInt64(QRTreasureId);

                Models.GeneratedModels.user_treasure_relation relation;

                // Connect to DB to link treasure found for hunt
                List<Models.GeneratedModels.user_treasure_relation> relations = database.user_treasure_relation
                    .Where(r => r.treasure_treasure_id == QRTreasureIdLong
                        && r.treasure_hunt_hunt_id == QRHuntIdLong
                        && r.user_user_id == session.user_id)
                    .ToList();

                if (relations.Count < 1)
                {
                    response["success"] = -2;
                    return Json(response);
                }
                else
                {
                    relation = relations.First();
                }


                // if the treasure has not been found, mark as found and update user points accordingly
                if (relation.found == "False")
                {
                    relation.found = true.ToString();

                    Models.GeneratedModels.treasure treasure = database.treasures
                        .Where(t => t.treasure_id == relation.treasure_treasure_id)
                        .First();

                    Models.GeneratedModels.user currentUser = database.users.Where(u => u.user_id == session.user_id).First();
                    currentUser.points += treasure.points; 
                    currentUser.num_treasures += 1;

                    // Check conditionals for award/badge/rank
                    BadgeController tmpController = new BadgeController();
                    tmpController.checkBadges();
                    tmpController.Dispose();

                }

                database.SaveChanges();

                response["huntId"] =  QRHuntId;
                response["treasureId"] = QRTreasureId;
                response["success"] = 1;

                return Json(response);
            }
            else
            {
                return Json(response);
            }

        }
    }
}