using Microsoft.AspNetCore.Components.Routing;
using NRCan.Datahub.Shared.EFCore;
using System.Threading.Tasks;

namespace NRCan.Datahub.Shared.Services
{
    public interface IUserLocationManagerService
    {
        Task DeleteUserRecent(string userId);
        Task<UserRecent> ReadRecentNavigations(string userId);
        Task RegisterNavigation(LocationChangedEventArgs eventArgs);
        Task RegisterNavigation(UserRecent recent);
    }
}