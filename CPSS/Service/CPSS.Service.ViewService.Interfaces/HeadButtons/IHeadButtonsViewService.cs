﻿using System.Collections.Generic;
using CPSS.Service.ViewService.ViewModels.HeadButtons.Request;
using CPSS.Service.ViewService.ViewModels.HeadButtons.Respond;

namespace CPSS.Service.ViewService.Interfaces.HeadButtons
{
    public interface IHeadButtonsViewService
    {
        List<RespondHeadButtonsViewModel> QueryHeadButtonsViewModelsByMenuID(RequestHeadButtonsViewModel request);
    }
}