using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace FindTheBooty.Controllers
{
    public class LeaderboardController : DataController
    {
        // GET: Leaderboard Page
        public ActionResult Index()
        {
            return RedirectToAction("Points");// View(database.users.OrderByDescending(x => x.points).ToList());
        }

        // GET: Points Leaderboard
        public ActionResult Points()
        {
                return View(database.users.OrderByDescending(x => x.points).ToList());  
        }
        /*
        // GET: Rank Leaderboard
        public ActionResult Rank()
        {
            return View(database.users.OrderByDescending(x => x.rank).ToList());
        }
        */
        // GET: Hunts Leaderboard
        public ActionResult Hunts()
        {
            return View(database.users.OrderByDescending(x => x.num_hunts).ToList());
        }

        // GET: Treasures Leaderboard
        public ActionResult Treasures()
        {
            return View(database.users.OrderByDescending(x => x.num_treasures).ToList());
        }
    }
}
