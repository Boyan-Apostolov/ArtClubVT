namespace ArtClubVT.Services.Data.Emails
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ArtClubVT.Common;
    using ArtClubVT.Services.Messaging;

    public class EmailsService : IEmailsService
    {
        private readonly IEmailSender emailSender;

        public EmailsService(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public async Task NotifyAdminForOrder(string userEmail)
        {
            await this.emailSender.SendEmailAsync(
                GlobalConstants.SystemEmail,
                "Нова поръчка!",
                GlobalConstants.SystemEmail,
                "Нова поръчка!",
                $"Здравей, шефе! Имаш нова поръчка от {userEmail}. Провери си таба: ПОРЪЧКИ");
        }

        public async Task ApproveOrderEmail(string userEmail)
        {
            await this.emailSender.SendEmailAsync(
                GlobalConstants.SystemEmail,
                GlobalConstants.SystemEmail,
                userEmail,
                "Потвърдена поръчка",
                "Вашата поръчка беше потвърдена, очаквайте я скоро. Може да видите повече инфо в секцията 'Моите поръчки'");
        }

        public async Task DeclineOrderEmail(string userEmail)
        {
            await this.emailSender.SendEmailAsync(
                GlobalConstants.SystemEmail,
                GlobalConstants.SystemEmail,
                userEmail,
                "Отказана поръчка",
                "Вашата поръчка беше отказана. За повече инфо моля, свържете се със собсвеника в секция 'Контакти'");
        }
    }
}
