﻿// Console app will replicate all documentation folder structure and try to translate the content of .md files using DeepL

// Usage > syncdocs <root-path>
// or    > dotnet run <root-path>

// Note: Expects an enviroment variable named "DEEPL_KEY" with the API key for DeepL

using SyncDocs;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: false)
    .AddUserSecrets<Program>();

var config = builder.Build();

// read the app config
var configParams = config.Get<ConfigParams>() ?? new();
var deeplKey = config.GetSection("DeepL")?.GetValue<string>("Key") ?? Environment.GetEnvironmentVariable("DEEPL_KEY") ?? string.Empty;

// get the source path
var sourcePath = args.Length == 1 ? args[0] : "./";

// file name cache
var fileNameCache = new FileNameCache(Path.Combine(sourcePath, configParams.Target, "filenamecache.json"));

// file mapping service
var fileMappingService = new FileMappingService();

// translation service
var translationService = new TranslationService(sourcePath, deeplKey);

// replication service
var replicationService = new ReplicationService(configParams, sourcePath, translationService, fileNameCache, fileMappingService);

// iterate the provided source folder
await IteratePath(sourcePath, BuildExcluder(configParams), replicationService);

// save translation cache
fileNameCache.SaveChanges();

// save the file mappings
fileMappingService.SaveTo(Path.Combine(sourcePath, "filemappings.json"));

// END

#region util functions

static async Task IteratePath(string path, Func<string, bool> excludeFunc, ReplicationService service)
{
    foreach (var dir in Directory.GetDirectories(path))
    {
        var name = Path.GetFileName(dir);

        // ignore all .* folders
        if (name.StartsWith('.'))
            continue;

        // check for excluded
        if (excludeFunc.Invoke(name))
            continue;

        service.AddFolder(dir);

        // check for exclusions
        await IteratePath(dir, n => false, service);
    }

    foreach (var file in Directory.GetFiles(path, "*.md"))
    {
        await service.AddFile(file);
    }
}

static Func<string, bool> BuildExcluder(ConfigParams config)
{
    var excludeSet = config.GetExcludedFolders().Select(s => s.ToLower()).ToHashSet();
    return n => excludeSet.Contains(n.ToLower());
}

#endregion