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
        public ActionResult ViewJoinedHunts()
        {
            Models.JoinedHuntList huntList = new Models.JoinedHuntList();
            return View(huntList.GetJoinedHunts());
        }

        // GET: Participating/ViewJoinableHunts
        public ActionResult ViewJoinableHunts(string error = null)
        {
            Models.JoinableHuntList huntList = new Models.JoinableHuntList();
            return View(huntList.GetJoinableHunts());
        }

        // GET: Participating/JoinHunt/{id of hunt to join}
        public ActionResult JoinHunt(int id = -1)
        {
            if (id == -1)
            {
                //TODO: Handle error and give feedback somehow
                return RedirectToAction("ViewJoinableHunts");
            }
            return View();
        }
    }
}