using Microsoft.AspNetCore.Mvc;

namespace Firma.Mvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
