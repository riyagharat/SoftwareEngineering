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
            return RedirectToAction("ViewHunts");
        }

        public ActionResult ViewHunts()
        {
            Models.ParticipatingHuntList huntList = new Models.ParticipatingHuntList();
            return View(huntList.GetParticipatingHunts());
        }
    }
}