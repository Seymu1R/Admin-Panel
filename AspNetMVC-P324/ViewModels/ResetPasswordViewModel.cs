using System.ComponentModel.DataAnnotations;

namespace AspNetMVC_P324.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmdPassword { get; set; }
    }
}
