using Microsoft.AspNetCore.Mvc;

namespace Assingment_2MVC.Controllers
{
    public abstract class AbstractBaseController : Controller
    {

        public void SetWelcome()
        {
            const string cookieName = "firstVisitDate";

            var firstVisitDate = HttpContext.Request.Cookies.ContainsKey(cookieName) && DateTime.TryParse(HttpContext.Request.Cookies[cookieName], out var parsedDate) ? parsedDate : DateTime.Now;


            var welcomeMessage = HttpContext.Request.Cookies.ContainsKey(cookieName)
                ? $"Welcome back! You first use this app on {firstVisitDate.ToShortDateString()}"
                : "Hey, Welcome to the Event Manager App!";

            var cookieOption = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30),
            };

            HttpContext.Response.Cookies.Append(cookieName,
                firstVisitDate.ToString(),
                cookieOption);
            ViewData["WelcomeMessage"] = welcomeMessage;
        }


      
    }
}
