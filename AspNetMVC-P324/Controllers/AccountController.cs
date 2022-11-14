using AspNetMVC_P324.Models.Entities.IdentityModel;
using AspNetMVC_P324.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMVC_P324.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult>Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)return View();
            var existuser = await _userManager.FindByNameAsync(model.Name);
            if (existuser != null) 
            {
                ModelState.AddModelError("", "UserName Tekrarlana bilmez");
                return View();
            }
            User user = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.UserName,
                Email = model.Email,
            };
            var result= await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(Index),"Home");
        }
    }
}
