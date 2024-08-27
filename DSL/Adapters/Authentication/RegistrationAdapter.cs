using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Authentication
{
    public class RegistrationAdapter
    {
        [Required(ErrorMessage = "Firstname is required")]
        [MaxLength(64, ErrorMessage = "Out of length limit")]
        [RegularExpression("[a-zA-z'-]+", ErrorMessage = "Not a word")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [MaxLength(64, ErrorMessage = "Out of length limit")]
        [RegularExpression("[a-zA-z'-]+", ErrorMessage = "Not a word")]
        public string? Lastname { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        //[RegularExpression("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "Not a correct email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Username is required due to log in procedure requirements")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required due to log in procedure requirements")]
        [DataType(DataType.Password)]
        [MinLength(12, ErrorMessage = "Password length of 12 symbols at least is required")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])[0-9]*.{12,}$", ErrorMessage = "Password must consist of at least one uppercase and one lowercase symbols")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Password check is required due to security reasons")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Check does not fit to entered password")]
        public string? PassCheck { get; set; }
    }
}