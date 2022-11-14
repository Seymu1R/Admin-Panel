using AspNetMVC_P324.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMVC_P324.ViewComponents
{
    public class ProductViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbcontext;

        public ProductViewComponent(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var products = await _dbcontext.Products.ToListAsync();
            return View();
        }
    }
}
