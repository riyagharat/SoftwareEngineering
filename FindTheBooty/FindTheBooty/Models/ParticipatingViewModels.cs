using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindTheBooty.Models
{
    public class Hunt
    {
        // hunt identifier information
        public int huntID { get; set; }
        public int sponsorID { get; set; }
        public string huntName { get; set; }

        // hunt type information
        public string huntType { get; set; } 
        public int timeCreate { get; set; }
        public int timeExpire { get; set; }
        public int maxNumOfUsers { get; set; }
        public bool multiOrSingle { get; set; }

        // calculated timeLeft in seconds
        public string endDateTime { get; set; }
    }

    public class JoinedHuntList
    {

        public List<Hunt> GetJoinedHunts()
        {
            // hunts joined and participating
            //TODO: Implement DB call to retrieve participating hunts
            List<Hunt> huntList = new List<Hunt>();
            Hunt huntItem = new Hunt();
            huntItem.huntID = 1;
            huntItem.huntName = "Bootylicious";
            huntItem.huntType = "Timed Free-for-all";
            huntItem.timeCreate = 1490376653;
            huntItem.timeExpire = 1494547200;

            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(huntItem.timeExpire).ToLocalTime();

            huntItem.endDateTime = dtDateTime.ToString("MM/dd/yyyy HH:mm");

            huntList.Add(huntItem);

            Hunt huntItem2 = new Hunt();
            huntItem2.huntID = 2;
            huntItem2.huntName = "Bootylicious 2.0";
            huntItem2.huntType = "Timed Sequential";
            huntItem2.timeCreate = 1490376653;
            huntItem2.timeExpire = 1494547260;

            System.DateTime dtDateTime2 = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime2.AddSeconds(huntItem2.timeExpire).ToLocalTime();

            huntItem2.endDateTime = dtDateTime.ToString("MM/dd/yyyy HH:mm");

            huntList.Add(huntItem2);

            return huntList;

        }
    }

    public class JoinableHuntList
    {
        // hunts available
        public List<Hunt> GetJoinableHunts()
        {
            // hunts joined and participating
            //TODO: Implement DB call to retrieve joinable hunts
            List<Hunt> huntList = new List<Hunt>();
            Hunt huntItem = new Hunt();
            huntItem.huntID = 3;
            huntItem.huntName = "Bootylicious 3.0";
            huntItem.huntType = "Timed Free-for-all";
            huntItem.timeCreate = 1490376653;
            huntItem.timeExpire = 1494547320;

            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(huntItem.timeExpire).ToLocalTime();

            huntItem.endDateTime = dtDateTime.ToString("MM/dd/yyyy HH:mm");

            huntList.Add(huntItem);

            Hunt huntItem2 = new Hunt();
            huntItem2.huntID = 4;
            huntItem2.huntName = "Bootylicious 4.0";
            huntItem2.huntType = "Timed Sequential";
            huntItem2.timeCreate = 1490376653;
            huntItem2.timeExpire = 1494547380;

            System.DateTime dtDateTime2 = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime2.AddSeconds(huntItem2.timeExpire).ToLocalTime();

            huntItem2.endDateTime = dtDateTime.ToString("MM/dd/yyyy HH:mm");

            huntList.Add(huntItem2);

            return huntList;

        }
    }
}
