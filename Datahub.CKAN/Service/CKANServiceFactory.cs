using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Datahub.CKAN.Service
{
    public class CKANServiceFactory : ICKANServiceFactory
    {
        readonly IOptions<CKANConfiguration> _ckanConfiguration;
        readonly IHttpClientFactory _httpClientFactory;
        readonly ILogger<CKANServiceFactory> _logger;

        public CKANServiceFactory(IHttpClientFactory httpClientFactory, IOptions<CKANConfiguration> ckanConfiguration, ILogger<CKANServiceFactory> logger)
        {
            _ckanConfiguration = ckanConfiguration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public ICKANService CreateService() => new CKANService(_httpClientFactory.CreateClient("DatahubApp"), _ckanConfiguration, _logger);

        public bool IsStaging() => (_ckanConfiguration.Value.BaseUrl ?? "").Contains("staging");
    }
}