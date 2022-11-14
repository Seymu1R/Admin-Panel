namespace AspNetMVC_P324.Models.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int DiscountPersantege { get; set; }       
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public double Discount(double price , int discount)
        {           
            double disprice = price - (price * discount) / 100;
            return disprice;
        }
    }
}
