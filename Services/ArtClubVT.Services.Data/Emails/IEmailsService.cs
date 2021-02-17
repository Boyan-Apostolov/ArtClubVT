namespace ArtClubVT.Services.Data.Emails
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmailsService
    {
        Task NotifyAdminForOrder(string userEmail);

        Task ApproveOrderEmail(string userEmail);

        Task DeclineOrderEmail(string userEmail);
    }
}
