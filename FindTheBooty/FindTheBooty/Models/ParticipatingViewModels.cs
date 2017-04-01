using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindTheBooty.Models
{
    public class Hunt
    {
        // hunt identifier information
        public int HuntID { get; set; }
        public int SponsorID { get; set; }
        public string HuntName { get; set; }

        // hunt type information
        public string HuntType { get; set; } 
        public int TimeCreate { get; set; }
        public int TimeExpire { get; set; }
        public int MaxNumOfUsers { get; set; }
        public bool MultiOrSingle { get; set; }

        // calculated timeLeft in seconds
        public string EndDateTime { get; set; }
    }

    public class JoinedHuntList
    {

        public bool DoHuntError { get; set; }   // used for invalid hunt access
        public List<Hunt> HuntList { get; set; }

        public List<Hunt> GetJoinedHunts()
        {
            // hunts joined and participating
            //TODO: Implement DB call to retrieve participating hunts

            // START SAMPLE POPULATION
            List<Hunt> huntList = new List<Hunt>();
            Hunt huntItem = new Hunt();
            huntItem.HuntID = 1;
            huntItem.HuntName = "Bootylicious";
            huntItem.HuntType = "Timed Free-for-all";
            huntItem.TimeCreate = 1490376653;
            huntItem.TimeExpire = 1494547200;

            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(huntItem.TimeExpire).ToLocalTime();

            huntItem.EndDateTime = dtDateTime.ToString("MM/dd/yyyy HH:mm");

            huntList.Add(huntItem);

            Hunt huntItem2 = new Hunt();
            huntItem2.HuntID = 2;
            huntItem2.HuntName = "Bootylicious 2.0";
            huntItem2.HuntType = "Timed Sequential";
            huntItem2.TimeCreate = 1490376653;
            huntItem2.TimeExpire = 1494547260;

            System.DateTime dtDateTime2 = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime2.AddSeconds(huntItem2.TimeExpire).ToLocalTime();

            huntItem2.EndDateTime = dtDateTime.ToString("MM/dd/yyyy HH:mm");

            huntList.Add(huntItem2);
            // END SAMPLE POPULATION

            return huntList;

        }
    }

    public class JoinableHuntList
    {

        // flag used specifically to determine if error needs to be shown on ViewJoinableHunts
        public bool JoinHuntError { get; set; }
        public List<Hunt> HuntList { get; set; }

        // hunts available
        public List<Hunt> GetJoinableHunts()
        {
            // hunts joined and participating
            //TODO: Implement DB call to retrieve joinable hunts

            // START SAMPLE POPULATION
            List<Hunt> huntList = new List<Hunt>();
            Hunt huntItem = new Hunt();
            huntItem.HuntID = 3;
            huntItem.HuntName = "Bootylicious 3.0";
            huntItem.HuntType = "Timed Free-for-all";
            huntItem.TimeCreate = 1490376653;
            huntItem.TimeExpire = 1494547320;

            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(huntItem.TimeExpire).ToLocalTime();

            huntItem.EndDateTime = dtDateTime.ToString("MM/dd/yyyy HH:mm");

            huntList.Add(huntItem);

            Hunt huntItem2 = new Hunt();
            huntItem2.HuntID = 4;
            huntItem2.HuntName = "Bootylicious 4.0";
            huntItem2.HuntType = "Timed Sequential";
            huntItem2.TimeCreate = 1490376653;
            huntItem2.TimeExpire = 1494547380;

            System.DateTime dtDateTime2 = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime2.AddSeconds(huntItem2.TimeExpire).ToLocalTime();

            huntItem2.EndDateTime = dtDateTime.ToString("MM/dd/yyyy HH:mm");

            huntList.Add(huntItem2);
            // END SAMPLE POPULATION

            return huntList;

        }
    }
}
