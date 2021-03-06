using Datahub.Core.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datahub.Core.Services
{
    public class PowerBiDataService : IPowerBiDataService
    {
        private readonly IDbContextFactory<DatahubProjectDBContext> _contextFactory;
        private readonly ILogger<PowerBiDataService> _logger;
        private readonly IMiscStorageService _miscStorageService;
        private readonly IDatahubAuditingService _auditingService;

        private static readonly string GLOBAL_POWERBI_ADMIN_LIST_KEY = "GlobalPowerBiAdmins";

        public PowerBiDataService(
            IDbContextFactory<DatahubProjectDBContext> contextFactory,
            ILogger<PowerBiDataService> logger,
            IMiscStorageService miscStorageService,
            IDatahubAuditingService auditingService)
        {
            _contextFactory = contextFactory;
            _logger = logger;
            _miscStorageService = miscStorageService;
            _auditingService = auditingService;
        }

        public async Task<IList<PowerBi_Workspace>> GetAllWorkspaces()
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var results = await ctx.PowerBi_Workspaces.Include(w => w.Project).ToListAsync();
            return results;
        }

        public async Task<bool> AddOrUpdateCataloguedWorkspaces(IEnumerable<PowerBi_WorkspaceDefinition> workspaceDefinitions)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();

            return await DoUpdateWorkspaces(ctx, workspaceDefinitions);
        }

        private async Task<bool> DoUpdateWorkspaces(DatahubProjectDBContext ctx, IEnumerable<PowerBi_WorkspaceDefinition> workspaceDefinitions)
        {
            foreach (var def in workspaceDefinitions)
            {
                var workspace = await ctx.PowerBi_Workspaces.FirstOrDefaultAsync(w => w.Workspace_ID == def.WorkspaceId);
                if (workspace == null)
                {
                    _logger.LogDebug("Creating workspace record for {} ({})", def.WorkspaceName, def.WorkspaceId);

                    workspace = new PowerBi_Workspace()
                    {
                        Workspace_ID = def.WorkspaceId,
                        Workspace_Name = def.WorkspaceName,
                        Sandbox_Flag = def.SandboxFlag,
                        Project_Id = def.ProjectId
                    };

                    ctx.PowerBi_Workspaces.Add(workspace);
                }
                else
                {
                    _logger.LogDebug("Updating workspace record for {} ({})", def.WorkspaceName, def.WorkspaceId);

                    workspace.Workspace_Name = def.WorkspaceName;
                    workspace.Sandbox_Flag = def.SandboxFlag;
                    workspace.Project_Id = def.ProjectId;
                }
            }

            try
            {
                await ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding/updating PowerBI workspaces");
                return false;
            }
        }

        public async Task<IList<PowerBi_DataSet>> GetAllDatasets()
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var results = await ctx.PowerBi_DataSets.ToListAsync();
            return results;
        }

        public async Task<bool> AddOrUpdateCataloguedDatasets(IEnumerable<PowerBi_DataSetDefinition> datasetDefinitions)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();

            return await DoUpdateDatasets(ctx, datasetDefinitions);
        }

        private async Task<bool> DoUpdateDatasets(DatahubProjectDBContext ctx, IEnumerable<PowerBi_DataSetDefinition> datasetDefinitions)
        {
            foreach (var def in datasetDefinitions)
            {
                var dataset = await ctx.PowerBi_DataSets.FirstOrDefaultAsync(d => d.DataSet_ID == def.DataSetId);
                if (dataset == null)
                {
                    _logger.LogDebug("Creating dataset record for {} ({}) in workspace {}", def.DataSetName, def.DataSetId, def.WorkspaceId);

                    dataset = new()
                    {
                        DataSet_ID = def.DataSetId,
                        DataSet_Name = def.DataSetName,
                        Workspace_Id = def.WorkspaceId
                    };

                    ctx.PowerBi_DataSets.Add(dataset);
                }
                else
                {
                    _logger.LogDebug("Updating dataset {} ({})", def.DataSetName, def.DataSetId);

                    dataset.DataSet_ID = def.DataSetId;
                    dataset.DataSet_Name = def.DataSetName;
                    dataset.Workspace_Id = def.WorkspaceId;
                }
            }

            try
            {
                await ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding/updating PowerBI datasets");
                return false;
            }
        }

        public async Task<IList<PowerBi_Report>> GetAllReports()
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var reports = await ctx.PowerBi_Reports.ToListAsync();
            return reports;
        }

        public async Task<bool> AddOrUpdateCataloguedReports(IEnumerable<PowerBi_ReportDefinition> reportDefinitions)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();

            return await DoUpdateReports(ctx, reportDefinitions);
        }

        private async Task<bool> DoUpdateReports(DatahubProjectDBContext ctx, IEnumerable<PowerBi_ReportDefinition> reportDefinitions)
        {
            foreach (var def in reportDefinitions)
            {
                var report = await ctx.PowerBi_Reports.FirstOrDefaultAsync(d => d.Report_ID == def.ReportId);
                if (report == null)
                {
                    _logger.LogDebug("Creating report record for {} ({}) in workspace {}", def.ReportName, def.ReportId, def.WorkspaceId);

                    report = new()
                    {
                        Report_ID = def.ReportId,
                        Report_Name = def.ReportName,
                        Workspace_Id = def.WorkspaceId
                    };

                    ctx.PowerBi_Reports.Add(report);
                }
                else
                {
                    _logger.LogDebug("Updating report {} ({})", def.ReportName, def.ReportId);

                    report.Report_ID = def.ReportId;
                    report.Report_Name = def.ReportName;
                    report.Workspace_Id = def.WorkspaceId;
                }
            }

            try
            {
                await ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding/updating PowerBI reports");
                return false;
            }
        }

        public async Task<List<PowerBi_Report>> GetReportsForProject(string projectCode, bool includeSandbox = false)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();

            var results = await ctx.PowerBi_Reports
                .Include(r => r.Workspace)
                .ThenInclude(w => w.Project)
                .Where(r => r.Workspace.Project != null 
                    && r.Workspace.Project.Project_Acronym_CD.ToLower() == projectCode.ToLower()
                    && (includeSandbox || !r.Workspace.Sandbox_Flag))
                .ToListAsync();
            return results;
        }

        public async Task<List<PowerBi_Report>> GetReportsForUser(string userId)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();

            var results = await ctx.PowerBi_Reports
                .Include(r => r.Workspace)
                .ThenInclude(w => w.Project)
                .ThenInclude(p => p.Users)
                .Where(r => r.Workspace.Project != null && r.Workspace.Project.Users.Any(u => u.User_ID == userId))
                .Distinct()
                .ToListAsync();

            return results;
        }

        public async Task<bool> BulkAddOrUpdatePowerBiItems(IEnumerable<PowerBi_WorkspaceDefinition> workspaceDefinitions, 
            IEnumerable<PowerBi_DataSetDefinition> datasetDefinitions, 
            IEnumerable<PowerBi_ReportDefinition> reportDefinitions)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var transaction = await ctx.Database.BeginTransactionAsync();

            var success = true;

            success &= await DoUpdateWorkspaces(ctx, workspaceDefinitions);
            success &= await DoUpdateDatasets(ctx, datasetDefinitions);
            success &= await DoUpdateReports(ctx, reportDefinitions);

            if (success)
            {
                await transaction.CommitAsync();
            }
            else
            {
                await transaction.RollbackAsync();
            }

            return success;
        }

        public async Task<PowerBi_Workspace> GetWorkspaceById(Guid id)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var result = await ctx.PowerBi_Workspaces.FirstOrDefaultAsync(w => w.Workspace_ID == id);
            return result;
        }

        public async Task<PowerBi_DataSet> GetDatasetById(Guid id)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var result = await ctx.PowerBi_DataSets.FirstOrDefaultAsync(d => d.DataSet_ID == id);
            return result;
        }

        public async Task<PowerBi_Report> GetReportById(Guid id)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var result = await ctx.PowerBi_Reports.FirstOrDefaultAsync(r => r.Report_ID == id);
            return result;
        }

        public async Task<List<string>> GetGlobalPowerBiAdmins()
        {
            var result = await _miscStorageService.GetObject<List<string>>(GLOBAL_POWERBI_ADMIN_LIST_KEY);
            return result ?? new List<string>();
        }

        public async Task SetGlobalPowerBiAdmins(IEnumerable<string> adminEmails)
        {
            var adminList = adminEmails.ToList();
            await _miscStorageService.SaveObject(adminList, GLOBAL_POWERBI_ADMIN_LIST_KEY);
        }

        public async Task<List<PowerBi_Report>> GetReportsForProjectWithExternalReportInfo(string projectCode, bool includeSandbox = false)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();

            var results = await ctx.PowerBi_Reports
                .Include(r => r.Workspace)
                .ThenInclude(w => w.Project)
                .Where(r => r.Workspace.Project != null
                    && r.Workspace.Project.Project_Acronym_CD.ToLower() == projectCode.ToLower()
                    && (includeSandbox || !r.Workspace.Sandbox_Flag))
                .ToListAsync();

            var externalReportIds = await ctx.ExternalPowerBiReports.Where(r => r.End_Date >= DateTime.Now).Select(r => r.Report_ID).ToListAsync();

            foreach (var report in results)
            {
                if (externalReportIds.Contains(report.Report_ID))
                    report.IsExternalReportActive = true;
            }

            return results;
        }

        public async Task CreateExternalPowerBiReportRequest(string userId, Guid reportId)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();

            var request = new ExternalPowerBiReport
            {
                RequestingUser = userId,
                Report_ID = reportId
            };

            ctx.ExternalPowerBiReports.Add(request);

            var result = await ctx.TrackSaveChangesAsync(_auditingService);

        }
        
        public async Task<bool> RevokePowerBiReportRequest(Guid reportId)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();

            var existing = await ctx.ExternalPowerBiReports.FirstOrDefaultAsync(t => t.Report_ID == reportId);
            var found = false;
            if (existing != null)
            {
                ctx.Remove(existing);
                found = true;
            }

            var result = await ctx.TrackSaveChangesAsync(_auditingService);
            return found;

        }


        public async Task<ExternalPowerBiReport> GetExternalReportRecord(Guid reportId)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();

            return ctx.ExternalPowerBiReports.FirstOrDefault(r => r.Report_ID == reportId);

        }

        public async Task<List<ExternalPowerBiReport>> GetRequestedExternalReports()
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();

            return ctx.ExternalPowerBiReports.Where(r => !r.Is_Created).ToList();
        }

        public async Task UpdateExternalPowerBiRecord(ExternalPowerBiReport report)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var rep = ctx.ExternalPowerBiReports.Where(r => report.ExternalPowerBiReport_ID == r.ExternalPowerBiReport_ID).FirstOrDefault();
            if (rep != null) 
            {
                rep.Url = report.Url;
                rep.Token = report.Token;
                rep.End_Date = report.End_Date;
                rep.Is_Created = report.Is_Created;
                rep.ValidationSalt = report.ValidationSalt;
                rep.Validation_Code = report.Validation_Code;
                await ctx.TrackSaveChangesAsync(_auditingService);
            }
        }
    }
}
