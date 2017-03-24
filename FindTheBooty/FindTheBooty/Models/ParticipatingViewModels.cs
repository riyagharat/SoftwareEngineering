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
        public int huntType { get; set; } 
        public int timeCreate { get; set; }
        public int timeExpire { get; set; }
        public int maxNumOfUsers { get; set; }
        public bool multiOrSingle { get; set; }
    }

    public class ParticipatingHuntList
    {
        // hunts joined and participating
    }

    public class AvailableHuntList
    {
        // hunts available
    }
}
