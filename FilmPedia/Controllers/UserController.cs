using FilmPedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace FilmPedia.Controllers
{
    public class UserController : Controller
    {
        private FilmPediaDBEntities db = new FilmPediaDBEntities();
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            // E-posta formatını kontrol etmek için sorgu
            bool isValidEmail = IsValidEmail(user.Email);

            if (!isValidEmail)
            {
                ModelState.AddModelError("Email", "Geçerli bir e-posta adresi giriniz!!!");
                // Eğer e-posta formatı geçerli değilse kullanıcıyı uyarmak için bir hata mesajı
                return View(user); // Kayıt formunu tekrar göster
            }

            // Kullanıcıyı veritabanına kaydetme işlemleri
            using (var db = new FilmPediaDBEntities()) // FilmPediaDBEntities, veritabanı bağlantı sınıfının adını temsil eder
            {
                // Kullanıcı adı veya e-posta ile var olan bir kullanıcıyı kontrol et
                bool isUserExists = db.Users.Any(u => u.Username == user.Username || u.Email == user.Email);

                if (isUserExists)
                {
                    // Kullanıcı zaten varsa uyarı mesajı gönder veya gerekli işlemleri gerçekleştir
                    ModelState.AddModelError("", "Bu kullanıcı adı veya e-posta zaten kullanılıyor.");
                    return View(user); // Kayıt formunu tekrar göster
                }

                // Kullanıcıyı veritabanına kaydetme işlemleri
                db.Users.Add(user); // Kullanıcıyı veritabanına ekleme
                db.SaveChanges(); // Değişiklikleri kaydetme
            }



            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            // Kullanıcı giriş kontrolü ve oturum açma işlemleri
            // Kullanıcının giriş bilgilerini veritabanından kontrol etme işlemleri
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Giriş başarılı ise, gerekli işlemleri gerçekleştir ve yönlendir
                Session["UserID"] = user.Id;
                TempData["LoggedInUserId"] = user.Id;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Geçersiz e-posta veya parola."); // Hata mesajı ekleme
                return View();
            }

        }

        public ActionResult Logout()
        {
            // Oturumu kapatma işlemleri
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        public ActionResult Dashboard()
        {
            // Kullanıcının giriş yaptıktan sonra yönlendirileceği sayfa

            return View();
        }
    }
}