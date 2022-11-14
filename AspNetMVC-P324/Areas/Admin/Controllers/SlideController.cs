using AspNetMVC_P324.Areas.Admin.Models;
using AspNetMVC_P324.DAL;
using AspNetMVC_P324.Models.Entities;
using AspNetMVC_P324.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMVC_P324.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SlideController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public  async Task<IActionResult> Index()
        {
            
            var slider = _dbContext.Sliders.SingleOrDefault();
           

          
            return View(slider);
        }
        public async Task<IActionResult> Update() 
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(SlideUpdateModel model)
        {
            var slider = _dbContext.Sliders.SingleOrDefault();
            slider.Title=model.Title;
            slider.Subtitle = model.Subtitle;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
