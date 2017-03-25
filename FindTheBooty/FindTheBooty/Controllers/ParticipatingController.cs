using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindTheBooty.Controllers
{
    public class ParticipatingController : Controller
    {

        // GET: Participating
        public ActionResult Index()
        {
            return RedirectToAction("ViewJoinedHunts");
        }

        // GET: Participating/ViewJoinedHunts
        public ActionResult JoinedHunts()
        {
            Models.JoinedHuntList joined = new Models.JoinedHuntList();
            joined.HuntList = joined.GetJoinedHunts();

            return View(joined);
        }

        // GET: Participating/ViewJoinableHunts
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
                //TODO: Handle error and give feedback somehow
                return RedirectToAction("JoinableHunts", new { error = true});
            }
            return View();
        }
    }
}