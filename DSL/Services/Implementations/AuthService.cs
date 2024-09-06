using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

using System.Net.Mail;
using System.Security.Claims;
using System.Text;

using DAL;

using DSL.Adapters.Authentication;
using DSL.Services.Declarations;

namespace DSL.Services.Implementations
{
    public class AuthService(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IMailerService mailerService) : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMailerService _mailerService = mailerService;

        public async Task<bool> InternalSignInAsync(AuthenticationAdapter userData)
        {
            ApplicationUser? user = await _userManager
                .FindByNameAsync(userData.Username!);

            if (user is null) return false;
            if (!(await _signInManager.CheckPasswordSignInAsync(
                user, userData.Password, false)).Succeeded) return false;

            if (_signInManager.IsSignedIn(_signInManager.Context.User))
                await SignOutAsync();

            await _signInManager
                .PasswordSignInAsync(user, userData.Password, false, false);

            return true;
        }

        public async Task<bool> ExternalSignInAsync()
        {
            ExternalLoginInfo xUserInfo = await _signInManager
                .GetExternalLoginInfoAsync();

            if (xUserInfo == null) return false;
            if (await _userManager.FindByEmailAsync(xUserInfo.Principal
                .FindFirstValue(ClaimTypes.Email)!) is not null) return false;

            if (_userManager.FindByEmailAsync(xUserInfo.Principal
                .FindFirstValue(ClaimTypes.Email)!) is null)
            {
                ApplicationUser user = new()
                {
                    Firstname = xUserInfo.Principal
                    .FindFirstValue(ClaimTypes.GivenName),
                    Lastname = xUserInfo.Principal
                    .FindFirstValue(ClaimTypes.Surname),
                    Email = xUserInfo.Principal
                    .FindFirstValue(ClaimTypes.Email),
                    UserName = "x-" + new MailAddress(xUserInfo.Principal
                    .FindFirstValue(ClaimTypes.Email)!).User
                };

                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "Customer");
                await _userManager.AddLoginAsync(user, xUserInfo);

                await _signInManager.ExternalLoginSignInAsync(
                    xUserInfo.LoginProvider, xUserInfo.ProviderKey, false);
            }
            else await _signInManager.ExternalLoginSignInAsync(
                xUserInfo.LoginProvider, xUserInfo.ProviderKey, false);

            return true;
        }

        public async Task<bool> InternalSignUpAsync(RegistrationAdapter userData)
        {
            ApplicationUser newUser = new()
            {
                Firstname = userData.Firstname,
                Lastname = userData.Lastname,
                Email = userData.Email,
                UserName = userData.UserName,
                PhoneNumber = userData.PhoneNumber
            };

            if (await _userManager.FindByNameAsync(newUser.UserName!)
                is not null) return false;
            if (await _userManager.FindByEmailAsync(newUser.Email!)
                is not null) return false;

            await _userManager.CreateAsync(newUser, userData.Password!);
            await _userManager.AddToRoleAsync(newUser, "Customer");

            return true;
        }

        public async Task<bool> BeginPasswordRecoveryAsync(RecoveryRequestAdapter recoveryData,
            string url, string message)
        {
            ApplicationUser? user = await _userManager
                .FindByNameAsync(recoveryData.Username!);

            if (user is null) return false;

            DateTime date = DateTime.Now;
            StringBuilder fullUrl = new();

            fullUrl.AppendFormat("{0}?username={1}&token={2}",
                url, recoveryData.Username,
                await _userManager.GeneratePasswordResetTokenAsync(user));

            string subject = "Recover password to Marketplace Account";
            string preparedMessage = _mailerService.PrepareMailHtml(message, "STUB",
                user.UserName!, date, date.AddDays(1D), fullUrl);

            await _mailerService.MailAsync(user.UserName!, user.Email!,
                subject, preparedMessage);

            return true;
        }

        public async Task<bool> EndPasswordRecoveryAsync(PasswordResetAdapter resetData)
        {
            ApplicationUser? user = await _userManager
                .FindByNameAsync(resetData.Username!);

            if (user is null) return false;
            if (!(await _userManager.ResetPasswordAsync(
                    user, resetData.Token!, resetData.Password!)).Succeeded) return false;

            return true;
        }

        public async Task<string> GetFullname(string username)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            if (user is null) return string.Empty;

            return string.Join(" ", user.Firstname, user.Lastname);
        }

        public AuthenticationProperties GetXAuthProps(string provider,
            string returnUrl) => _signInManager
            .ConfigureExternalAuthenticationProperties(provider, returnUrl);

        public async Task<IEnumerable<AuthenticationScheme>> GetXAuthSchemesAsync()
            => await _signInManager.GetExternalAuthenticationSchemesAsync();

        public async Task SignOutAsync() => await _signInManager.SignOutAsync();
    }
}