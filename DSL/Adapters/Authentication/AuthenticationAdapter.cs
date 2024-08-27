using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Authentication
{
    public class AuthenticationAdapter
    {
        [Required(ErrorMessage = "Username is required due to log in procedure requirements")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required due to log in procedure requirements")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}