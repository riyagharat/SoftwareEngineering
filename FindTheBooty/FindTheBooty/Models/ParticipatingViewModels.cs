using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindTheBooty.Models
{
    // Generic Hunt Model
    public class Hunt
    {
        // hunt identifier information
        public long HuntID { get; set; }
        public long SponsorID { get; set; }
        public string HuntName { get; set; }

        // hunt type information
        public string HuntType { get; set; } 
        public string SeqOrFFA { get; set; }
        public System.DateTime TimeCreate { get; set; }
        public System.DateTime TimeExpire { get; set; }
        public int MaxNumOfUsers { get; set; }
        public bool MultiOrSingle { get; set; }
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
