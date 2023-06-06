using FilmPedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPedia.Controllers
{
    public class FavoriteListController : Controller
    {
        // GET: FavoriteList
        [CustomAuthorize]
        public ActionResult Index()
        {
            int userId = 0;

            if (TempData["LoggedInUserId"] != null)
            {
                userId = (int)TempData["LoggedInUserId"];
            }
            else if (Session["UserID"] != null)
            {
                userId = (int)Session["UserID"];
            }

            FilmPediaDBEntities db = new FilmPediaDBEntities();
            var favoriteList = db.UserFavoriteLists.Where(f => f.User_ID == userId).ToList();

            return View(favoriteList);


        }
    }
}