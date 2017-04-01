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
            return View();
        }

        // GET: Participating/JoinedHunts
        public ActionResult JoinedHunts(bool error = false)
        {
            Models.JoinedHuntList joined = new Models.JoinedHuntList();
            if (error)
                joined.DoHuntError = true;
            joined.HuntList = joined.GetJoinedHunts();

            return View(joined);
        }

        // GET: Participating/JoinableHunts
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
                return RedirectToAction("JoinableHunts", new { error = true});
            }
            return View();
        }

        // GET: Participating/DoHunt{id of hunt to continue in}
        public ActionResult DoHunt(int id = -1)
        {
            if (id < 0)
            {
                return RedirectToAction("JoinedHunts", new { error = true });
            }
            return View();
        }
    }
}