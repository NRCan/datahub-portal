﻿using Datahub.Core.Configuration;
using Datahub.Core.Model.Datahub;
using Datahub.Core.Services.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Datahub.ProjectTools.Catalog
{

#nullable enable
    public class DHDatabricksResource : DHURLResource
    { 

        private readonly IDbContextFactory<DatahubProjectDBContext> dbFactoryProject;
        private readonly bool _isServiceConfigured;
        private readonly bool isServiceConfigured;

        public DHDatabricksResource(IDbContextFactory<DatahubProjectDBContext> dbFactoryProject,            
            IOptions<DataProjectsConfiguration> configuration)
        {
            this.dbFactoryProject = dbFactoryProject;
            _isServiceConfigured = configuration.Value.Databricks;
        }

        private bool _databricksServiceRequested = false;
        private bool _databricksServiceCreated = false;

        protected override async Task InitializeAsync(string userId, Microsoft.Graph.User graphUser, bool isProjectAdmin)
        {
            await using var projectDbContext = await dbFactoryProject.CreateDbContextAsync();
            var serviceRequests = Project.ServiceRequests;
            _databricksServiceRequested = serviceRequests.Any(r => r.ServiceType == IRequestManagementService.DATABRICKS && r.Is_Completed == null);
            _databricksServiceCreated = !string.IsNullOrEmpty(Project.Databricks_URL);
            parameters.Add(nameof(Databricks.Project), Project);
        }

        protected override string Title => "Azure Databricks";
        protected override string Description => "Run your Python, R and SQL notebooks in the cloud with Databricks for analytics, machine learning and data pipelines";
        protected override string Icon => "/icons/svg/databricks.svg";
        protected override bool IsIconSVG => true;

        protected override Type ComponentType => typeof(Databricks);

        protected override bool IsServiceRequested => _databricksServiceRequested && !_databricksServiceCreated;

        protected override bool IsServiceConfigured => _isServiceConfigured;

        protected override bool IsServiceAvailable => _databricksServiceCreated;

        public override string[] GetTags()
        {
            return new[] { "Databricks", "Jupyter Notebooks", "Analytics" };
        }


    }
#nullable disable
}