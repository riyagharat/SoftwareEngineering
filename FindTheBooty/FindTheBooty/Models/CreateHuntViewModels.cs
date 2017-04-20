//CreateHuntViewModels
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindTheBooty.Models
{
    public class HuntSettingsViewModel
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

    public class AddTreasuresViewModal
    {
        public int HuntRelID { get; set; }
        public int TreasureID { get; set; }
        public string Description { get; set; }
        public int Seq_Order { get; set; }
        public bool Confirmation { get; set; }
        public int Points { get; set; }
    }

}
