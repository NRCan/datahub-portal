using System.Threading.Tasks;
using NRCan.Datahub.Shared.Data.FGP;

namespace NRCan.Datahub.Shared.Services
{
    public interface IFGPSearchService
    {
        public Task<GeoCoreSearchResult> SearchFGPByKeyword(string keyword, int min = 1, int max = 10, string lang = "en");
    }
}