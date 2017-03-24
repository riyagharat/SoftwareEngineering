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

        public ActionResult ViewJoinedHunts()
        {
            Models.JoinedHuntList huntList = new Models.JoinedHuntList();
            return View(huntList.GetJoinedHunts());
        }

        public ActionResult ViewJoinableHunts()
        {
            Models.JoinableHuntList huntList = new Models.JoinableHuntList();
            return View(huntList.GetJoinableHunts());
        }
    }
}