using Microsoft.CodeAnalysis.Options;
using System.ComponentModel.DataAnnotations;

namespace AspNetMVC_P324.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmedPasword { get; set; }
    }
}
