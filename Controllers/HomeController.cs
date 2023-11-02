using Microsoft.AspNetCore.Mvc;

namespace posts_cs.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
