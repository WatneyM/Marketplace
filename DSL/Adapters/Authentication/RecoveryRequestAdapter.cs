using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Authentication
{
    public class RecoveryRequestAdapter
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
    }
}