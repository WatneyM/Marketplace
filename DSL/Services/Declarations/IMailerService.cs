namespace DSL.Services.Declarations
{
    public interface IMailerService
    {
        public Task MailAsync(string destUser,
            string destEmail, string subject, string htmlMessage);

        public string PrepareMailHtml(string htmlMessage,
            string selector, params object[] insertFrom);
    }
}