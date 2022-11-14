using AspNetMVC_P324.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMVC_P324.Controllers
{
    public class ErorController : Controller
    {
        public IActionResult Error1(int code)
        {
            ErorViewModel eror = new ErorViewModel()
            {
                StatusCode = HttpContext.Response.StatusCode,
                Title = HttpContext.Response.Headers.ToString()
                
            };
        
           
            return View(eror);
        }
    }
}
