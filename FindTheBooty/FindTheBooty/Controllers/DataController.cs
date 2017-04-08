using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindTheBooty.Controllers
{
    public class DataController : Controller
    {
        public Models.GeneratedModels.FindTheBooty_prodEntities database;

        public DataController() : base()
        {
            this.database = new Models.GeneratedModels.FindTheBooty_prodEntities();
        }
    }
}