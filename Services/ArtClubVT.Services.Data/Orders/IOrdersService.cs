﻿namespace ArtClubVT.Services.Data.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ArtClubVT.Web.ViewModels;

    public interface IOrdersService
    {
        Task<int> CreateOrder(AddOrderViewModel model);
    }
}
