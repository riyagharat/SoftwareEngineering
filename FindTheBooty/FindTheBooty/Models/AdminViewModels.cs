using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindTheBooty.Models
{
    // View Model used to upgrade a user to Creator
    public class UpgradeUserViewModel
    {
        [Display(Name = "Find a First Mate:")]
        public string AdminInput { get; set; }
    }
}