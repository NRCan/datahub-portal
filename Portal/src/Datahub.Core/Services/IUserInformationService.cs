﻿using Microsoft.Graph;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Datahub.Core.Services;

public interface IUserInformationService
{
    Task<User> GetCurrentGraphUserAsync();
    Task<User> GetGraphUserAsync(string userId);
    Task<User> GetAnonymousGraphUserAsync();
    Task<string> GetUserIdString();
    Task<bool> HasUserAcceptedTAC();
    Task<bool> RegisterUserTAC();
    Task<bool> RegisterUserLanguage(string language);
    Task<string> GetUserLanguage();
    Task<bool> IsFrench();        
    Task<string> GetUserEmailDomain();
    Task<string> GetUserEmailPrefix();
    bool SetLanguage(string language);
    Task<string> GetUserRootFolder();
    Task<bool> IsUserWithoutInitiatives();
    Task<bool> IsViewingAsGuest();
    Task<bool> IsViewingAsVisitor();
    Task SetViewingAsGuest(bool isGuest);
    Task SetViewingAsVisitor(bool isVisitor);
    Task<ClaimsPrincipal> GetAuthenticatedUser(bool forceReload = false);
    Task<bool> IsUserProjectAdmin(string projectAcronym);

    Task<bool> IsUserProjectMember(string projectAcronym);

    Task<bool> IsUserDatahubAdmin();

}

public static class UserInformationServiceConstants
{
    public static readonly string ANONYMOUS_USER_ID = "c90acba3-26e4-471d-bbdf-544906e6a980";
    public static readonly string ANONYMOUS_USER_NAME = "Anonymous User";
    public static readonly string ANONYMOUS_USER_EMAIL = "anyone@example.com";

    private static User _anonymousUser;
    public static User GetAnonymousUser()
    {
        if (_anonymousUser == null)
        {
            _anonymousUser = new User()
            {
                Id = ANONYMOUS_USER_ID,
                Mail = ANONYMOUS_USER_EMAIL,
                DisplayName = ANONYMOUS_USER_NAME,
                UserPrincipalName = ANONYMOUS_USER_EMAIL
            };
        }
        return _anonymousUser;
    }
}