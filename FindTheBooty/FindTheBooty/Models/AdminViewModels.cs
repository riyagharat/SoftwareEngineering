using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindTheBooty.Models
{
    public class UpgradeUserViewModel
    {
        [Display(Name = "Find a First Mate:")]
        public string AdminInput { get; }

        public GeneratedModels.user ReturnedUser { get; set; }
    }
}