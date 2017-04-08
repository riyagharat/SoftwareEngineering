using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindTheBooty.Controllers
{
    public class ParticipatingController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FTBConnection"].ConnectionString;

        // GET: Participating
        public ActionResult Index()
        {
            return RedirectToAction("JoinedHunts");
        }

        // GET: Participating/JoinedHunts
        public ActionResult JoinedHunts(bool error = false)
        {
            using (System.Data.SqlClient.SqlConnection db = new System.Data.SqlClient.SqlConnection(connectionString))
            using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("", db))
            {
                db.Open();
                command.CommandText = "SELECT * FROM dbo.user_hunt_relation WHERE user_user_id=@Id";
                command.Parameters.AddWithValue("@Id", 1);
                System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //TODO: loop, building list of Hunts for JoinedHuntList model
                    string val1 = reader["user_user_id"].ToString(); // grab first column value
                    string val2 = reader["hunt_hunt_id"].ToString();
                    string val3 = reader["active"].ToString();
                    string val4 = reader["completed"].ToString();
                }
            }
            Models.JoinedHuntList joined = new Models.JoinedHuntList();
            if (error)
                joined.DoHuntError = true;
            joined.HuntList = joined.GetJoinedHunts();

            return View(joined);
        }

        // GET: Participating/JoinableHunts
        public ActionResult JoinableHunts(bool error = false)
        {
            Models.JoinableHuntList joinable = new Models.JoinableHuntList();
            if (error)
                joinable.JoinHuntError = true;
            joinable.HuntList = joinable.GetJoinableHunts();
            return View(joinable);
        }

        // GET: Participating/JoinHunt/{id of hunt to join}
        public ActionResult JoinHunt(int id = -1)
        {
            if (id < 0)
            {
                return RedirectToAction("JoinableHunts", new { error = true});
            }
            return View();
        }

        // GET: Participating/DoHunt{id of hunt to continue in}
        public ActionResult DoHunt(int id = -1)
        {
            if (id < 0)
            {
                return RedirectToAction("JoinedHunts", new { error = true });
            }
            return View();
        }

        // POST: Participating/UploadQR
        [HttpPost]
        public ActionResult UploadQR()
        {
            // initialize response object
            /* success value description:
                -1 -- Invalid format/QR Code not found
                0  -- Image failed to upload
                1  -- Upload/processing success
            */
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

                //TODO: Connect to DB to link treasure found for hunt
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