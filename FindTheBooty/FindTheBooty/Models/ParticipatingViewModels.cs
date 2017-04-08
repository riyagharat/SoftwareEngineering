using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindTheBooty.Models
{
    // Generic Hunt Model
    public class Hunt
    {
        // hunt identifier information
        public int HuntID { get; set; }
        public int SponsorID { get; set; }
        public string HuntName { get; set; }

        // hunt type information
        public string HuntType { get; set; } 
        public System.DateTime TimeCreate { get; set; }
        public System.DateTime TimeExpire { get; set; }
        public int MaxNumOfUsers { get; set; }
        public bool MultiOrSingle { get; set; }

        // calculated timeLeft in seconds
        public string EndDateTime { get; set; }
    }

    // Generic Treasure Model
    public class Treasure
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Confirmation { get; set; }
        public int Points { get; set; }
        public bool Found { get; set; }
        public string Image { get; set; } // to be used only for storing image path (calculated)
    }

    public class JoinedHuntList
    {

        public bool DoHuntError { get; set; }   // used for invalid hunt access
        public List<Hunt> HuntList { get; set; }

        // Poll DB for hunts joined and participating
        public List<Hunt> GetJoinedHunts()
        {
            HuntList = new List<Models.Hunt>();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FTBConnection"].ConnectionString;

            // open db connection and build list of hunts
            using (System.Data.SqlClient.SqlConnection db = new System.Data.SqlClient.SqlConnection(connectionString))
            using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("", db))
            {
                db.Open();
                command.CommandText = 
                    "SELECT hunt.hunt_id, hunt.hunt_name, hunt.hunt_type, hunt.time_expire " +
                    "FROM dbo.hunt hunt, dbo.user_hunt_relation relation " + 
                    "WHERE(relation.hunt_hunt_id = hunt.hunt_id AND relation.user_user_id = @Id); ";
                command.Parameters.AddWithValue("@Id", 1); //TODO: Replace with user ID from Session/Cookie
                System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FindTheBooty.Models.Hunt hunt = new FindTheBooty.Models.Hunt();
                    //TODO: loop, building list of Hunts for JoinedHuntList model
                    hunt.HuntID = System.Convert.ToInt32(reader["hunt_id"]);
                    hunt.HuntName = reader["hunt_name"].ToString();
                    hunt.HuntType = reader["hunt_type"].ToString();
                    hunt.TimeExpire = System.Convert.ToDateTime(reader["time_expire"].ToString());
                    HuntList.Add(hunt);
                }
            }

            // return list of hunts the user has joined
            return HuntList;

        }
    }

    public class JoinableHuntList
    {

        // flag used specifically to determine if error needs to be shown on ViewJoinableHunts
        public bool JoinHuntError { get; set; }
        public List<Hunt> HuntList { get; set; }

        // hunts available
        //TODO: Remove after obtaining DB Connection
        public List<Hunt> GetJoinableHunts()
        {
            // hunts joined and participating
            //TODO: Implement DB call to retrieve joinable hunts

            // START SAMPLE POPULATION
            List<Hunt> huntList = new List<Hunt>();
            Hunt huntItem = new Hunt();
            huntItem.HuntID = 1;
            huntItem.HuntName = "Bootylicious 3.0";
            huntItem.HuntType = "Timed Free-for-all";
            huntItem.TimeCreate = new System.DateTime(2017, 04, 08);
            huntItem.TimeExpire = new System.DateTime(2017, 04, 20);

            huntList.Add(huntItem);

            Hunt huntItem2 = new Hunt();
            huntItem2.HuntID = 2;
            huntItem2.HuntName = "Bootylicious 4.0";
            huntItem2.HuntType = "Timed Sequential";
            huntItem2.TimeCreate = new System.DateTime(2017, 04, 08);
            huntItem2.TimeExpire = new System.DateTime(2017, 04, 20);

            huntList.Add(huntItem2);
            // END SAMPLE POPULATION

            return huntList;

        }
    }

    // used in the DoHunt action
    public class DoHuntList
    {
        public Hunt Hunt { get; set; }  // hunt information
        public List<Treasure> TreasureList { get; set; } // list of treasures for the hunt
    }
}
