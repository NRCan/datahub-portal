using Microsoft.AspNetCore.Components.Routing;
using NRCan.Datahub.Shared.EFCore;
using System.Threading.Tasks;

namespace NRCan.Datahub.Shared.Services
{
    public class OfflineUserLocationManagerService : IUserLocationManagerService
    {
        public Task DeleteUserRecent(string userId)
        {
            return Task.FromResult(0);
        }

        public Task<UserRecent> ReadRecentNavigations(string userId)
        {
            return Task.FromResult(new UserRecent());
        }

        public Task RegisterNavigation(LocationChangedEventArgs eventArgs)
        {
            return Task.FromResult(0);
        }

        public Task RegisterNavigation(UserRecent recent)
        {
            return Task.FromResult(0);
        }
    }
}
