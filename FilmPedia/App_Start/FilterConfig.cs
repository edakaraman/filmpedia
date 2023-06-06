using System.Web;
using System.Web.Mvc;

namespace FilmPedia
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // UserID oturum verisine erişim
            var userId = httpContext.Session["UserID"] as int?;
            if (!userId.HasValue)
            {
                // UserID oturum verisi bulunamazsa veya uygun şekilde alınamazsa
                // giriş yapma işlemi yeniden gerekebilir
                httpContext.Response.Redirect("~/User/Login");
                return false;
            }

            // Diğer yetkilendirme kontrolleri...

            return true;
        }
    }
}
