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
        FilmPediaDBEntities db=new FilmPediaDBEntities();
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

        [CustomAuthorize]
   
        public ActionResult DeleteFavFilm(string filmTitle)
        {
            int loggedInUserId = 0;

            // LoggedInUserId değerini alın
            if (TempData["LoggedInUserId"] != null)
            {
                loggedInUserId = (int)TempData["LoggedInUserId"];
            }
            else if (Session["UserID"] != null)
            {
                loggedInUserId = (int)Session["UserID"];
            }

            if (loggedInUserId == 0)
            {
                // LoggedInUserId değeri bulunamadığında uygun bir hata işleme mekanizması kullanabilirsiniz.
                return Json(new { success = false, message = "LoggedInUserId değeri bulunamadı." });
            }

            try
            {
                using (var db = new FilmPediaDBEntities())
                {
                    // Kullanıcı Favori Listeleri tablosundan, verilen userId ve filmTitle'a sahip kaydı ara
                    var favoriteFilm = db.UserFavoriteLists.FirstOrDefault(f => f.User_ID == loggedInUserId && f.FavoriteFilm == filmTitle);

                    if (favoriteFilm != null)
                    {
                        // Kayıt bulunduğunda, kaydı veritabanından sil
                        db.UserFavoriteLists.Remove(favoriteFilm);
                        db.SaveChanges();

                        // Silme işlemi başarılı olduysa
                        return Json(new { success = true });

                    }
                    else
                    {
                        // Kayıt bulunamadığında
                        return Json(new { success = false, message = "Verilen film favori listenizde bulunamadı." });
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda
                return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu: " + ex.Message });
            }

        }

    }
}