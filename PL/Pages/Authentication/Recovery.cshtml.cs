using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Authentication;
using DSL.Services.Declarations;

namespace PL.Pages.Authentication
{
    [BindProperties]
    public class RecoveryModel(IAuthService service) : PageModel
    {
        private readonly IAuthService _service = service;

        public RecoveryRequestAdapter RecoveryData { get; set; } = new();
        public PasswordResetAdapter ResetData { get; set; } = new();

        public async Task<IActionResult> OnPostResetPassword()
        {
            await _service.EndPasswordRecoveryAsync(ResetData);

            return Redirect("/authentication");
        }

        public async Task<IActionResult> OnPostSendRecoveryMailAsync()
        {
            if (ModelState.IsValid)
            {
                return Redirect("/authentication");
            }

            await _service.BeginPasswordRecoveryAsync(RecoveryData, new Uri(Request.GetDisplayUrl()).GetLeftPart(UriPartial.Path),
                System.IO.File.ReadAllText("Pages/Mail/RecoveryMail.html"));

            return Redirect("/authentication");
        }
    }
}