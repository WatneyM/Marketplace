using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Authentication;
using DSL.Services.Declarations;

namespace PL.Pages.Authentication
{
    [BindProperties]
    public class RegistrationModel(IAuthService service) : PageModel
    {
        private readonly IAuthService _service = service;

        public RegistrationAdapter AuthData { get; set; } = new();
        public IEnumerable<AuthenticationScheme> Schemes { get; set; } = [];

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            if (await _service.InternalSignUpAsync(AuthData))
            {
                return Redirect("/authentication");
            }
            else return null!;
        }
    }
}