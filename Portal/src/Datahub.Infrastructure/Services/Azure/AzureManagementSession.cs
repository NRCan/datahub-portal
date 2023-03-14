﻿using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Datahub.Infrastructure.Services.Azure;

public class AzureManagementSession
{
    private readonly IAzureServicePrincipalConfig _configuration;
    private readonly HttpClient _httpClient;
    private readonly string _accessToken;
    private readonly CancellationToken _cancellationToken;

    public AzureManagementSession(IAzureServicePrincipalConfig configuration, HttpClient httpClient, string accessToken, CancellationToken cancellationToken)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _accessToken = accessToken;
        _cancellationToken = cancellationToken;
    }

    public string AccessToken => _accessToken;

    public async Task<double?> GetResourceGroupLastYearCost(string resourceGroup)
    {
        var url = GetCostManagementRequestUrl(resourceGroup);
        var request = GetYearCostRequest();
        var response = await _httpClient.PostAsync<CostManagementResponse>(url, _accessToken, GetStringContent(request), _cancellationToken);
        return GetResourceGroupCost(response);
    }

    public async Task<List<AzureDailyCost>?> GetResourceGroupMonthlyCostPerDay(string resourceGroup)
    {
        var url = GetCostManagementRequestUrl(resourceGroup);
        var request = GetMonthlyCostPerDayRequest();
        var response = await _httpClient.PostAsync<CostManagementResponse>(url, _accessToken, GetStringContent(request), _cancellationToken);
        return GetResourceGroupCostPerDay(response);
    }

    public async Task<double?> GetResourceGroupYesterdayCost(string resourceGroup)
    {
        var url = GetCostManagementRequestUrl(resourceGroup);
        var request = GetYesterdayTotalCostRequest();
        var response = await _httpClient.PostAsync<CostManagementResponse>(url, _accessToken, GetStringContent(request), _cancellationToken);
        return GetResourceGroupCost(response);
    }

    public async Task<List<AzureServiceCost>?> GetResourceGroupYearCostByService(string resourceGroup)
    {
        var url = GetCostManagementRequestUrl(resourceGroup);
        var request = GetYearCostByServiceRequest();
        var response = await _httpClient.PostAsync<CostManagementResponse>(url, _accessToken, GetStringContent(request), _cancellationToken);
        return GetServiceCosts(response, nameIndex: 1);
    }

    public async Task<List<AzureServiceCost>?> GetResourceGroupYesterdayCostByService(string resourceGroup)
    {
        var url = GetCostManagementRequestUrl(resourceGroup);
        var request = GetYesterdayCostByServiceRequest();
        var response = await _httpClient.PostAsync<CostManagementResponse>(url, _accessToken, GetStringContent(request), _cancellationToken);
        return GetServiceCosts(response, nameIndex: 1);
    }

    static StringContent GetStringContent(object content)
    {
        var jsonContent = JsonSerializer.Serialize(content, GetJsonSerializerOptions());
        return new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
    }

    static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new() 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    private string GetCostManagementRequestUrl(string resourceGroup)
    {
        var version = "2021-10-01";
        return $"{AzureManagementUrls.ManagementUrl}/subscriptions/{_configuration.SubscriptionId}/resourceGroups/{resourceGroup}/providers/Microsoft.CostManagement/query?api-version={version}&$top=5000";
    }

    static CostManagementRequest GetYearCostRequest()
    {
        var (from, to) = GetLastYearPeriod();
        return new()
        {
            Type = "ActualCost",
            DataSet = new()
            {
                Granularity = "None",
                Aggregation = GetCostAggregation()
            },
            Timeframe = "Custom",
            TimePeriod = new()
            {
                From = from,
                To = to
            }
        };
    }

    static CostManagementRequest GetMonthlyCostPerDayRequest()
    {
        return new()
        {
            Type = "ActualCost",
            DataSet = new()
            {
                Granularity = "Daily",
                Aggregation = GetCostAggregation()
            }
        };
    }

    static CostManagementRequest GetYesterdayTotalCostRequest()
    {
        var (from, to) = GetYesterdayPeriod();
        return new()
        {
            Type = "ActualCost",
            DataSet = new()
            {
                Granularity = "None",
                Aggregation = GetCostAggregation()
            },
            Timeframe = "Custom",
            TimePeriod = new()
            {
                From = from,
                To = to
            }
        };
    }

    static CostManagementRequest GetYearCostByServiceRequest()
    {
        var (from, to) = GetLastYearPeriod();
        return new()
        {
            Type = "ActualCost",
            DataSet = new()
            {
                Granularity = "None",
                Aggregation = GetCostAggregation(),
                Grouping = new()
                {
                    new()
                    {
                        Type = "Dimension",
                        Name = "ServiceName"
                    }
                }
            },
            Timeframe = "Custom",
            TimePeriod = new()
            {
                From = from,
                To = to
            }
        };
    }

    static CostManagementRequest GetYesterdayCostByServiceRequest()
    {
        var (from, to) = GetYesterdayPeriod();
        return new()
        {
            Type = "ActualCost",
            DataSet = new()
            {
                Granularity = "None",
                Aggregation = GetCostAggregation(),
                Grouping = new()
                {
                    new()
                    {
                        Type = "Dimension",
                        Name = "ServiceName"
                    }
                }
            },
            Timeframe = "Custom",
            TimePeriod = new()
            {
                From = from,
                To = to
            }
        };
    }

    static CostManagementRequestAggregation GetCostAggregation() => new()
    {
        TotalCost = new()
        {
            Name = "Cost",
            Function = "Sum"
        }
    };

    static double? GetResourceGroupCost(CostManagementResponse? response)
    {
        if (response is null ||
            response.Properties is null ||
            response.Properties.Rows is null ||
            response.Properties.Rows.Count == 0 ||
            response.Properties.Rows[0].Count == 0)
            return default;

        return ParseDouble(response.Properties.Rows[0][0]);
    }

    static List<AzureDailyCost>? GetResourceGroupCostPerDay(CostManagementResponse? response)
    {
        return response?.Properties?.Rows?.Select(r => new AzureDailyCost()
        {
            Date = ParseDate(r[1]),
            Cost = ParseDouble(r[0]),
        }).ToList();
    }

    static List<AzureServiceCost>? GetServiceCosts(CostManagementResponse? response, int nameIndex)
    {
        return response?.Properties?.Rows?.Select(r => new AzureServiceCost() 
        { 
            Name = ParseString(r[nameIndex]),
            Cost = ParseDouble(r[0])
        })
        .Where(c => c.Cost > 0.01)
        .OrderByDescending(c => c.Cost)
        .ToList();
    }

    static (DateTime from, DateTime to) GetLastYearPeriod()
    {
        var to = DateTime.Now;
        var from = to.AddYears(-1).AddHours(1);
        return (from, to);
    }

    static (DateTime from, DateTime to) GetYesterdayPeriod()
    {
        var from = DateTime.Now.AddDays(-1).Date;
        var to = from.AddHours(24).AddSeconds(-1);
        return (from, to);
    }

    static string ParseString(object o) => o?.ToString() ?? string.Empty;

    static double ParseDouble(object o) => double.TryParse(o.ToString(), out double v) ? v : default;

    static DateTime ParseDate(object o)
    {
        var strValue = o.ToString();
        return DateTime.ParseExact(strValue!, "yyyyMMdd", CultureInfo.InvariantCulture).Date;
    }
}