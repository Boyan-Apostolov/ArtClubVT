using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArtClubVT.Common;
using ArtClubVT.Services.Messaging;

namespace ArtClubVT.Services.Data.Emails
{
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
    }
}
