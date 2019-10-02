using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}