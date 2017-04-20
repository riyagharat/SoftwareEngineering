using FindTheBooty.Models;
using System.Linq;
using System.Web.Mvc;

namespace FindTheBooty.Controllers
{
    [Authorize]
    public class AccountController : DataController
    {

        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else if (!this.checkLogin(model))
            {
                // Login doesn't exist
                ModelState.AddModelError("", "Invalid Login");
                return View(model);
            }
            setUser(getUser(model));
            // Model is valid successful login
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.GeneratedModels.user newUser = new Models.GeneratedModels.user();
                newUser.email = model.Email;
                newUser.display_name = model.DisplayName;
                newUser.password = model.Password;
                //Initialize Model with null items
                newUser.first_name = model.FirstName;
                newUser.last_name = model.LastName;
                newUser.gender = "";
                newUser.phone = System.Convert.ToInt64(model.PhoneNumber);
                newUser.points = 0;
                newUser.rank = "";
                newUser.num_hunts = 0;
                newUser.num_treasures = 0;
                newUser.user_type = "User";

                // Add User to database by adding primary key
                var latestUser = database.users.OrderBy(u => u.user_id ?? int.MaxValue.ToString()).ToList().Last();
                newUser.user_id = (int.Parse(latestUser.user_id) + 1).ToString();

                // Log user in and commit to database
                setUser(newUser);
                database.users.Add(newUser);
                database.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            // tear down session info and redirect to splash screen
            Session["LoggedUser"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home", new { LoggedOut = true });
        }

        protected bool checkLogin(LoginViewModel model)
        {
            return (database.users.Any(user => user.email == model.Email && user.password == model.Password));
        }

        protected Models.GeneratedModels.user getUser(LoginViewModel model)
        {
            if (checkLogin(model))
            {
                Models.GeneratedModels.user loginUser = database.users.Single(x => x.email == model.Email && x.password == model.Password);
                return loginUser;
            }
            else
            {
                throw new System.Exception("No login exists");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }

        [AllowAnonymous]
        public ActionResult UserProfile()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UserID = (Models.GeneratedModels.user)Session["LoggedUser"];
                Models.GeneratedModels.user user = database.users.Where(x => x.user_id == UserID.user_id).ToList().First();
                user.email = model.Email;
                user.display_name = model.ConfirmEmail;
                //Initialize Model with null items
                user.first_name = model.FirstName;
                user.last_name = model.LastName;
                user.phone = System.Convert.ToInt64(model.PhoneNumber);
                user.user_type = "User";
                // save to database
                setUser(user);
                database.SaveChanges();

                return RedirectToAction("UserProfile", "Account");
            }
            return View(model);
        }
    }
}