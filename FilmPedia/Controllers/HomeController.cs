using FilmPedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FilmPedia.Controllers
{
    public class HomeController : Controller
    {
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
            return View();
        }


        public class FavoriteMovieModel
        {
            public string FavoriteFilm { get; set; }
           
        }

   //     Bu model, FavoriteFilm ve User_ID adında iki özelliğe sahip.JavaScript nesnesindeki özelliklerl

        
                [HttpPost]


        public ActionResult AddToFavorites(FavoriteMovieModel favoriteMovie)
        {
            UserFavoriteLists favoriteMovies = new UserFavoriteLists();
            
            using (var context = new FilmPediaDBEntities())
            {
                // Giriş yapmış kullanıcının IDsini alın
             // Bu kısmı kendi oturum yönetim mantığınıza göre düzenlemelisiniz
                favoriteMovies.FavoriteFilm = favoriteMovie.FavoriteFilm;
                favoriteMovies.User_ID = (int)Session["UserID"];
                // Favori filmi UserFavoriteLists tablosuna ekle

                var favoriteMoviesList = context.UserFavoriteLists.FirstOrDefault(u => u.User_ID == favoriteMovies.User_ID && u.FavoriteFilm == favoriteMovies.FavoriteFilm);

                if (favoriteMoviesList == null)
                {
                    context.UserFavoriteLists.Add(favoriteMovies);

                    context.SaveChanges();
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        [HttpPost]
        public JsonResult CheckFavoriteMovie(int userID, string movieTitle)
        {


         FilmPediaDBEntities db = new FilmPediaDBEntities();
        // Veritabanında giriş yapmış kullanıcının favori filmlerini kontrol edin
        var favoriteMovie = db.UserFavoriteLists.FirstOrDefault(u => u.User_ID == userID && u.FavoriteFilm == movieTitle);

            // Sonuçlara göre favori film listesinde bulunup bulunmadığını kontrol edin
            var exists = (favoriteMovie != null);

            // Sonucu JSON formatında döndürün
            return Json(new { exists });
        }

    }
}