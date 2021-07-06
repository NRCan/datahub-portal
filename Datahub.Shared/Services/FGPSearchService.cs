using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NRCan.Datahub.Shared.Data.FGP;

namespace NRCan.Datahub.Shared.Services
{
    public class FGPSearchService : IFGPSearchService
    {
        private static readonly string FGP_SEARCH_API_URL = "https://hqdatl0f6d.execute-api.ca-central-1.amazonaws.com/dev/geo";

        private readonly IHttpClientFactory _httpClient;
        
        private readonly ILogger<FGPSearchService> _logger;

        public FGPSearchService(ILogger<FGPSearchService> logger, IHttpClientFactory httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<GeoCoreSearchResult> SearchFGPByKeyword(string keyword, int min = 1, int max = 10, string lang = "en")
        {
            _logger.LogDebug($"Searching FGP with keyword '{keyword}' (min: {min} , max: {max}, lang: {lang})");

            try 
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri($"{FGP_SEARCH_API_URL}?keyword_only=true&lang={lang}&keyword={keyword}&min={min}&max={max}");

                    _logger.LogTrace($"URI: {request.RequestUri}");

                    using (var httpClient = _httpClient.CreateClient())
                    using (var response = await httpClient.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();

                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<GeoCoreSearchResult>(content);

                        _logger.LogDebug($"Got {result.Count} FGP results for '{keyword}'");

                        return result;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, $"HTTP Error searching FGP for '{keyword}'");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error searching FGP for '{keyword}'");
                throw;
            }
        }
    }
}