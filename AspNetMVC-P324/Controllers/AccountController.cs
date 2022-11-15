using AspNetMVC_P324.Data;
using AspNetMVC_P324.Models.Entities.IdentityModel;
using AspNetMVC_P324.Services;
using AspNetMVC_P324.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMVC_P324.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly IMailServices _mailmeneger;
        

        public AccountController(UserManager<User> userManager, SignInManager<User> signinManager, IMailServices mailService)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _mailmeneger= mailService;
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
            var existuser = await _userManager.FindByNameAsync(model.UserName);
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

            await _signinManager.SignInAsync(user, false);

            return RedirectToAction(nameof(Index),"Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existUser = await _userManager.FindByNameAsync(model.UserName);

                if (existUser == null)
                {
                    ModelState.AddModelError("", "UserName Yalnisdir");
                    return View();
                }
                
                var result = await _signinManager.PasswordSignInAsync(existUser, model.Password,false,true); 

                if (!result.Succeeded) 
                {
                    ModelState.AddModelError("", "Username ve ya Sifre Yalnisdir");
                    return View();
                }
               return RedirectToAction(nameof(Index),"Home");
            }
            return View();
        }
        public IActionResult ForgotPasword()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ForgotPasword(ForgotPaswordModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Mail yazilmalidir!");
            }
            var existuser = await _userManager.FindByEmailAsync(model.Email);
            if (existuser != null)
            {
                ModelState.AddModelError("", "Bele Email movcud deyil");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(existuser);
            var resetlink = Url.Action(nameof(ResetPassword), "Account", new { email = model.Email, token },Request.Scheme,Request.Host.ToString());
            var mailrequest = new RequestEmail 
            {
                ToEmail= model.Email,
                Body=resetlink,
                Subject="Reset Pasword",
            };

            await _mailmeneger.SendEmailAsync(mailrequest);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ResetPassword(string email, string token ) 
        { 
               
            return View(new ResetPasswordViewModel { Email = email, Token = token });  
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Email Qaydalara uygun doldurulmalidir"); 
            }

                User user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                    return BadRequest();

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                
            
            return View();

        }
    }
}
