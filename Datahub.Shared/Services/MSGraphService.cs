﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using NRCan.Datahub.Shared.Data;

namespace NRCan.Datahub.Shared.Services
{
    public class MSGraphService : IMSGraphService
    {
        private GraphServiceClient graphServiceClient;
        private IConfiguration _configuration;
        private IWebHostEnvironment _webHostEnvironment;
        private ILogger<MSGraphService> _logger;
        private IHttpClientFactory _httpClientFactory;

        public Dictionary<string, GraphUser> UsersDict { get; set; }

        public MSGraphService(IWebHostEnvironment webHostEnvironment, IConfiguration configureOptions, ILogger<MSGraphService> logger, IHttpClientFactory clientFactory)
        {
            //clientSecret = configuration["ClientAppSecret"];            
            _webHostEnvironment = webHostEnvironment;
            _configuration = configureOptions;
            _logger = logger;
            _httpClientFactory = clientFactory;
        }

        public GraphUser GetUser(string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                if (UsersDict != null && UsersDict.ContainsKey(userId))
                {
                    return UsersDict[userId];
                }
            }

            return null;
        }

        public string GetUserName(string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                var user = GetUser(userId);
                return user?.DisplayName ?? "...";
            }

            return "...";
        }

        public Dictionary<string, GraphUser> GetUsersList()
        {
            return UsersDict;
        }

        public async Task LoadUsersAsync()
        {
            try
            {
                if (UsersDict == null)
                {
                    _logger.LogInformation("Entering Log Users");
                    UsersDict = new Dictionary<string, GraphUser>();
                    PrepareAuthenticatedClient();

                    IGraphServiceUsersCollectionPage usersPage = await graphServiceClient.Users.Request().GetAsync();
    
                    // Add the first page of results to the user list                    
                    if (usersPage?.CurrentPage.Count > 0)
                    {
                        foreach (User user in usersPage)
                        {
                            var newUser = GraphUser.Create(user);
                            UsersDict.Add(newUser.Id, newUser);
                        }
                    }

                    // Fetch each page and add those results to the list
                    while (usersPage.NextPageRequest != null)
                    {
                        usersPage = await usersPage.NextPageRequest.GetAsync();
                        foreach (User user in usersPage)
                        {
                            var newUser = GraphUser.Create(user);
                            UsersDict.Add(newUser.Id, newUser);
                        }
                    }
                    _logger.LogInformation("Exiting Log Users");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error Loading Users Async");
            }

        }
        
        public async Task<Dictionary<string, GraphUser>> GetUsersAsync()
        {
            try
            {
                if (UsersDict == null)
                {
                    await LoadUsersAsync();
                }

                return UsersDict;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not get users. Err: {e}");
                _logger.LogError($"Could not get users. Err: {e}");
            }

            return new Dictionary<string, GraphUser>();
        }

        private void PrepareAuthenticatedClient()
        {
            try
            {
                IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                                .Create(Environment.GetEnvironmentVariable("AZURE_CLIENT_ID"))
                                .WithTenantId(Environment.GetEnvironmentVariable("AZURE_TENANT_ID"))
                                .WithClientSecret(Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET"))
                                .Build();
                ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);

                var httpClient = _httpClientFactory.CreateClient();
                var graphServiceClient = new GraphServiceClient(httpClient);

                graphServiceClient.AuthenticationProvider = authProvider;

            }
            catch (Exception e)
            {
                _logger.LogError($"Error preparing authentication client: {e.Message}");
                Console.WriteLine($"Error preparing authentication client: {e.Message}");
                throw;
            }
        }
    }
}
