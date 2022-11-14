using Microsoft.AspNetCore.Identity;

namespace AspNetMVC_P324.Models.Entities.IdentityModel
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
