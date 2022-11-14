using AspNetMVC_P324.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMVC_P324.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        private AppDbContext _dbContext;

        public FooterViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _dbContext.Bios.ToListAsync();
            return View();
        }
    }
}

