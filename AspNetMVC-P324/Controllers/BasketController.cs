using Microsoft.AspNetCore.Mvc;

namespace AspNetMVC_P324.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
