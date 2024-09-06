using Microsoft.AspNetCore.Authentication;

using DSL.Adapters.Authentication;

namespace DSL.Services.Declarations
{
    public interface IAuthService
    {
        public Task<bool> BeginPasswordRecoveryAsync(RecoveryRequestAdapter recoveryData,
            string url, string message);
        public Task<bool> EndPasswordRecoveryAsync(PasswordResetAdapter resetData);

        public Task<string> GetFullname(string username);

        public AuthenticationProperties GetXAuthProps(string provider,
            string returnUrl);

        public Task<IEnumerable<AuthenticationScheme>> GetXAuthSchemesAsync();

        public Task<bool> InternalSignInAsync(AuthenticationAdapter userData);
        public Task<bool> ExternalSignInAsync();

        public Task<bool> InternalSignUpAsync(RegistrationAdapter userData);

        public Task SignOutAsync();
    }
}