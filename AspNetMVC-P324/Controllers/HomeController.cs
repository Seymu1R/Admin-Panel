using AspNetMVC_P324.DAL;
using AspNetMVC_P324.Data;
using AspNetMVC_P324.Models.Entities;
using AspNetMVC_P324.Models.ViewModels;
using AspNetMVC_P324.Services;
using AspNetMVC_P324.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AspNetMVC_P324.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IMailServices _mailservice;



        public HomeController(AppDbContext dbContext, IMailServices mailservice)
        {
            _dbContext = dbContext;
            _mailservice = mailservice;
        }

        public IActionResult Index()
        {
            
            var sliderImages = _dbContext.SliderImages.ToList();
            var slider = _dbContext.Sliders.SingleOrDefault();
            var categories = _dbContext.Categories.ToList();
            var products = _dbContext.Products.ToList();

            var homeViewModel = new HomeViewModel
            {
                SliderImages = sliderImages,
                Slider = slider,
                Categories = categories,
                Products = products
            };

            return View(homeViewModel);
        }
        
        public async Task<IActionResult> AddBasket(int? id )
        {
            if (id is null || id == 0) return NotFound();
            Product product = await _dbContext.Products.FindAsync(id);
            if (product is null) return NotFound();
            string basketstr = HttpContext.Request.Cookies["Basket"];
            BasketVM basket;
            if (string.IsNullOrEmpty(basketstr))
            {
                basket = new BasketVM();
                BasketCookieVM basketcookie = new BasketCookieVM
                {
                    Id = product.Id,
                    Quantity = 1
                };
                basket.BasketCookieVM = new List<BasketCookieVM>();
                basket.BasketCookieVM.Add(basketcookie);
                basket.TotalPrice = product.Price;

            }
            else 
            {
                basket = JsonConvert.DeserializeObject<BasketVM>(HttpContext.Request.Cookies["Basket"]);
                BasketCookieVM existed = basket.BasketCookieVM.FirstOrDefault(p => p.Id == id);
                if (existed is null)
                {
                    BasketCookieVM basketcookie = new BasketCookieVM
                    {
                        Id = product.Id,
                        Quantity = 1
                    };
                    basket.BasketCookieVM.Add(basketcookie);
                    basket.TotalPrice += product.Price;
                }
                else 
                {
                    basket.TotalPrice += product.Price;
                    existed.Quantity++;
                }
            }

            basketstr = JsonConvert.SerializeObject(basket);
            HttpContext.Response.Cookies.Append("Basket", basketstr);
            return RedirectToAction(nameof(ShowBasket));
        }
        [HttpGet]
        public IActionResult ShowBasket()
        {
            
            if (HttpContext.Request.Cookies["Basket"]is null) return NotFound();
            var basketvm = JsonConvert.DeserializeObject<BasketVM>(HttpContext.Request.Cookies["Basket"]);
            List<Product> products = new List<Product>();
            foreach (var item in basketvm.BasketCookieVM)
            {
                 var product = _dbContext.Products.Find(item.Id);
                 products.Add(product);
                 basketvm.Products = products;
                
            }           
                           
            return Ok(basketvm);
        }
    }
}
