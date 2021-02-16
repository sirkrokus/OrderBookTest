using System;
using System.Web;

namespace WebApp.Controllers
{
    public class CookieHelper
    {
        private static readonly string USER_COOKIE = "_usr";
        
        public static HttpCookie SetUserCookie(HttpRequestBase request, HttpResponseBase response)
        {
            HttpCookie userCookie = request.Cookies[USER_COOKIE];
            if (userCookie == null || String.IsNullOrEmpty(userCookie.Value))
            {
                string uid = Guid.NewGuid().ToString("n").Substring(0, 8);
                userCookie = new HttpCookie(USER_COOKIE, uid);
                userCookie.Expires = DateTime.Now.AddDays(30);
                response.Cookies.Add(userCookie);
            }
            return userCookie;
        }
        
    }
}