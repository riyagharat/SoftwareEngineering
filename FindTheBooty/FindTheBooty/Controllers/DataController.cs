using FindTheBooty.Models.GeneratedModels;
using System;
using System.Web.Mvc;

namespace FindTheBooty.Controllers
{
    public class DataController : Controller
    {
        public FindTheBooty_prodEntities database;

        public DataController() : base()
        {
            this.database = new FindTheBooty_prodEntities();
        }
        
        /// <summary>
        /// Checks Session if a user has logged in.
        /// If there is a user logged in, return that user model.
        /// Otherwise, a blank model
        /// </summary>
        /// <returns>FindTheBooty.Model.GenerateModels.user object</returns>
        protected user getUser()
        {
            if (Session["LoggedUser"] != null)
            {
                return new user();
            }
            else
            {
                return (user)Session["LoggedUser"];
            }
        }

        /// <summary>
        /// Sets the loginUser object into the session
        /// </summary>
        /// <param name="loginUser">a logged in user</param>
        protected void setUser(user loginUser)
        {
            Session["LoggedUser"] = loginUser;
        }
    }
}