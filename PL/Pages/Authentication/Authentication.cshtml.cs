using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Authentication;
using DSL.Services.Declarations;

namespace PL.Pages.Authentication
{
    [BindProperties]
    public class AuthenticationModel(IAuthService service) : PageModel
    {
        private readonly IAuthService _service = service;

        public AuthenticationAdapter AuthData { get; set; } = new();

        public IActionResult OnGetRequestExternalSignIn(string provider, string returnUrl)
        {
            return Challenge(_service.GetXAuthProps(provider, returnUrl), provider);
        }

        public async Task<IActionResult> OnGetExternalSignInApprovedAsync()
        {
            if (await _service.ExternalSignInAsync())
            {
                return Redirect("/");
            }
            else return Redirect("/authentication");
        }

        public async Task<IActionResult> OnGetExternalSignInApprovedAndRedirectAsync(string ReturnUrl)
        {
            if (await _service.ExternalSignInAsync())
            {
                return Redirect(ReturnUrl);
            }
            else return RedirectToPage("/Authentication/Authentication",
                new { ReturnUrl });
        }

        public IActionResult OnGetSignOut()
        {
            Response.Cookies.Delete(".AspNetCore.Session");

            _service.SignOutAsync();

            return Redirect("/authentication");
        }

        public async Task<IActionResult> OnPostSignInAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (await _service.InternalSignInAsync(AuthData))
            {
                return Redirect("/");
            }
            else return Page();
        }

        public async Task<IActionResult> OnPostAuthorizeAsync(string ReturnUrl)
        {
            if (!ModelState.IsValid) return Page();

            if (await _service.InternalSignInAsync(AuthData))
            {
                return Redirect(ReturnUrl);
            }
            else return RedirectToPage("/Authentication/Authentication",
                new { ReturnUrl });
        }
    }
}