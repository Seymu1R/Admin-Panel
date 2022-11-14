using AspNetMVC_P324.Models.Entities;

namespace AspNetMVC_P324.ViewModels
{
    public class BasketVM
    {
        public List<BasketCookieVM> BasketCookieVM { get; set; }        
        public double TotalPrice { get; set; }
        public List<Product> Products { get; set; }
    }
}
