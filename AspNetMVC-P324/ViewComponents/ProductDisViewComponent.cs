using AspNetMVC_P324.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMVC_P324.ViewComponents
{
    public class ProductDisViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public ProductDisViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await  _dbContext.Products.ToListAsync();
            return View();
        }
    }
}
