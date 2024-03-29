using Microsoft.AspNetCore.Components;

namespace Datahub.Core.Services.Api;

public class BaseService
{
    protected NavigationManager _navigationManager;
    protected UIControlsService _uiService;

    public BaseService(NavigationManager navigationManager, UIControlsService uiService)
    {
        _navigationManager = navigationManager;
        _uiService = uiService;
    }
}