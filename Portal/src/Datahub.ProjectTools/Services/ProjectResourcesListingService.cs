﻿using Datahub.Core.Configuration;
using Datahub.Core.Model.Datahub;
using Datahub.Core.Services;
using Datahub.ProjectTools.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Datahub.ProjectTools.Services;

public static class ProjectResourcesListingServiceExtensions
{
    public static void AddProjectResources(this IServiceCollection services, IConfiguration configuration)
    {
        ProjectResourcesListingService.RegisterResources(services, configuration);
        services.AddScoped<ProjectResourcesListingService>();
    }
}

public class ProjectResourcesListingService
{
    private readonly IServiceProvider serviceProvider;
    private readonly IUserInformationService userInformationService;
    private readonly GitHubToolsService githubToolsService;
    private readonly IOptions<DataProjectsConfiguration> configuration;
    private readonly IDbContextFactory<DatahubProjectDBContext> dbFactoryProject;
    private readonly ILogger<ProjectResourcesListingService> logger;

    public ProjectResourcesListingService(IServiceProvider serviceProvider,
        IUserInformationService userInformationService,
        GitHubToolsService githubToolsService,
        IOptions<DataProjectsConfiguration> configuration,
        ILogger<ProjectResourcesListingService> logger,
        IDbContextFactory<DatahubProjectDBContext> dbFactoryProject)
    {
        this.serviceProvider = serviceProvider;
        this.userInformationService = userInformationService;
        this.githubToolsService = githubToolsService;
        this.configuration = configuration;
        this.dbFactoryProject = dbFactoryProject;
        this.logger = logger;
    }

   
    public static void RegisterResources(IServiceCollection services, IConfiguration configuration)
    {
        DataProjectsConfiguration dataProjectsConfiguration = configuration.GetSection(nameof(DataProjectsConfiguration)).Get<DataProjectsConfiguration>()!;
                
        foreach (var resource in GetAllResourceProviders(dataProjectsConfiguration))
            services.AddTransient(resource);
        services.AddTransient<DHGitModuleResource>();
    }

    public async Task<List<IProjectResource>> GetResourcesForProject(Datahub_Project project)
    {
        using var ctx = dbFactoryProject.CreateDbContext();
        //load requests for project
        ctx.Attach(project);
        await ctx.Entry(project)
            .Collection(b => b.ServiceRequests)
            .LoadAsync();

        await ctx.Entry(project)
            .Collection(b => b.Resources)
            .LoadAsync();

        var services = new ServiceCollection();

        using var serviceScope = serviceProvider.CreateScope();
        var authUser = await userInformationService.GetAuthenticatedUser();
        var output = new List<IProjectResource>();
        var isUserAdmin = await userInformationService.IsUserProjectAdmin(project.Project_Acronym_CD);
        var isUserDHAdmin = await userInformationService.IsUserDatahubAdmin();

        var allResourceProviders = GetAllResourceProviders(configuration.Value);

        foreach (var item in allResourceProviders)
        {
            logger.LogDebug($"Configuring {item.Name} in project {project.Project_ID} ({project.Project_Name})");
            try
            {
                var dhResource = serviceScope.ServiceProvider.GetRequiredService(item) as IProjectResource;
                if (dhResource != null)
                {
                    await dhResource.InitializeAsync(project, await userInformationService.GetUserIdString(),
                        await userInformationService.GetCurrentGraphUserAsync(), isUserAdmin || isUserDHAdmin);
                    output.Add(dhResource);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    $"Error configuring {item.Name} in project {project.Project_ID} ({project.Project_Name})");
            }
        }

        logger.LogTrace($"Git module repository enabled:{configuration.Value.EnableGitModuleRepository}");
        if (configuration.Value.EnableGitModuleRepository)
        {
            var githubModules = await githubToolsService.GetAllModules();
            foreach (var item in githubModules)
            {
                logger.LogDebug($"Configuring {item.Name} in project {project.Project_ID} ({project.Project_Name})");
                var gitModule = serviceScope.ServiceProvider.GetRequiredService<DHGitModuleResource>();
                gitModule.ConfigureGitModule(item);
                await gitModule.InitializeAsync(project, await userInformationService.GetUserIdString(),
                    await userInformationService.GetCurrentGraphUserAsync(), isUserAdmin || isUserDHAdmin);
                output.Add(gitModule);
            }
        }

        return output;
    }

    private static List<Type> GetAllResourceProviders(DataProjectsConfiguration config)
    {
        var allResourceProviders = new List<Type>();

        if (config.PowerBI)
        {
            allResourceProviders.Add(typeof(DHPowerBIResource));
        }

        if (config.PublicSharing)
        {
            allResourceProviders.Add(typeof(DHPublicSharing));
        }

        if (config.DataEntry)
        {
            allResourceProviders.Add(typeof(DHDataEntry));
        }

        // add legacy Databricks and Storage in case Git Modules are disabled
        if (!config.EnableGitModuleRepository)
        {
            allResourceProviders.Add(typeof(DHDatabricksResource));
            allResourceProviders.Add(typeof(DHStorageResource));
        }

        return allResourceProviders;
    }
}