using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Authentication
{
    public class PasswordResetAdapter
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(12, ErrorMessage = "Password length of 12 symbols at least is required")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])[0-9]*.{12,}$", ErrorMessage = "Password must consist of at least one uppercase and one lowercase symbols")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Password check is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Check does not fit to entered password")]
        public string? PassCheck { get; set; }

        public string? Username { get; set; }
        public string? Token { get; set; }
    }
}