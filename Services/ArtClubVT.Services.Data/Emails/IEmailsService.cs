using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArtClubVT.Services.Data.Emails
{
    public interface IEmailsService
    {
        Task NotifyAdminForOrder(string userEmail);
    }
}
