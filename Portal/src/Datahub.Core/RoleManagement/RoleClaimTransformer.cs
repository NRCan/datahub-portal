﻿using Microsoft.AspNetCore.Authentication;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Datahub.Core.Data;
using Datahub.Core.Services.Security;

namespace Datahub.Core.RoleManagement;

//https://stackoverflow.com/questions/58483620/net-core-3-0-claimstransformation
public class RoleClaimTransformer : IClaimsTransformation
{
    private readonly ServiceAuthManager serviceAuthManager;
    private readonly ILogger<RoleClaimTransformer> logger;

    public RoleClaimTransformer(ServiceAuthManager serviceAuthManager, ILogger<RoleClaimTransformer> logger)
    {
        this.serviceAuthManager = serviceAuthManager;
        this.logger = logger;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        try
        {
            var userName = principal.Identity.Name;
            var userId = principal.Claims.First(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            if (userId is null)
            {
                logger.LogCritical("user uid not available in claims");
            }
            else
            {
                var allProjects = serviceAuthManager.GetAllProjects();

                var authorizedProjects = await serviceAuthManager.GetUserAuthorizations(userId);
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, $"default"));
                foreach (var project in allProjects)
                {
                    if (await serviceAuthManager.IsProjectAdmin(userId, project))
                    {
                        if (project == RoleConstants.DATAHUB_ADMIN_PROJECT && serviceAuthManager.GetViewingAsGuest(userId))
                        {
                            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, RoleConstants.DATAHUB_ROLE_ADMIN_AS_GUEST));
                        } 
                        else
                        {
                            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, $"{project}{RoleConstants.ADMIN_SUFFIX}"));
                        }
                    }
                }
                foreach (var project in authorizedProjects)
                {
                    ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, project.Project_Acronym_CD));
                }
            }

        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Cannot load project permissions");
        }
        return principal;

    }

}