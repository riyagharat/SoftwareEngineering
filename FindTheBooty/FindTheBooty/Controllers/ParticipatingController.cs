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

        public ActionResult ViewHunts()
        {
            return View();
        }
    }
}