﻿using System.Threading.Tasks;

namespace Datahub.Core.Services.Security;

public interface IKeyVaultService
{
    Task<string> GetSecret(string secretName);
    Task<string> GetClientSecret();
    Task<string> EncryptApiTokenAsync(string tokenData);
    Task<string> DecryptApiTokenAsync(string tokenData);
}