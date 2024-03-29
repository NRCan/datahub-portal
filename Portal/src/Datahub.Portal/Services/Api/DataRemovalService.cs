﻿using Azure.Storage.Files.DataLake;
using Datahub.Core.Data;
using Datahub.Core.Services;
using Datahub.Core.Services.Api;
using Datahub.Core.Services.Storage;
using Microsoft.AspNetCore.Components;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace Datahub.Portal.Services.Api;

public class DataRemovalService : BaseService, IDataRemovalService
{
    private ILogger<DataRemovalService> _logger;
    private DataLakeClientService _dataLakeClientService;
    private DataRetrievalService _dataRetrievalService;

    public DataRemovalService(ILogger<DataRemovalService> logger,
        DataLakeClientService dataLakeClientService,
        DataRetrievalService dataRetrievalService,
        NavigationManager navigationManager,
        UIControlsService uiService)
        : base(navigationManager, uiService)
    {
        _logger = logger;
        _dataLakeClientService = dataLakeClientService;
        _dataRetrievalService = dataRetrievalService;
    }

    public async Task<bool> Delete(Folder folder, Microsoft.Graph.User currentUser)
    {
        try
        {
            // Deleting a folder means deleting all files underneath,
            // We need to mark these files as 'isdeleted' for indexer
            var directoryClient = await DeleteAllFilesUnderneath(folder, currentUser);
            var response = directoryClient.DeleteAsync();

            _logger.LogDebug($"Delete folder: {folder.fullPathFromRoot} user: {currentUser.DisplayName} SUCCEEDED.");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Delete folder: {folder.fullPathFromRoot} user: {currentUser.DisplayName} FAILED.");
            throw;
        }
    }

    public async Task<bool> Delete(FileMetaData file, Microsoft.Graph.User currentUser)
    {
        try
        {
            var fileSystemClient = await _dataLakeClientService.GetDataLakeFileSystemClient();
            var directoryClient = fileSystemClient.GetDirectoryClient(file.folderpath);
            DataLakeFileClient fileClient = directoryClient.GetFileClient(file.filename);

            var result = await DeleteFileClient(fileClient, file, currentUser);
            var status = result ? "SUCCEEDED" : "FAILED";

            _logger.LogDebug($"Delete file: {file.folderpath}/{file.filename} user: {currentUser.DisplayName} {status}.");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Delete file: {file.folderpath}/{file.filename} user: {currentUser.DisplayName} FAILED.");
            throw;
        }
    }

    protected async Task<DataLakeDirectoryClient> DeleteAllFilesUnderneath(Folder folder, Microsoft.Graph.User currentUser)
    {
        try
        {
            // Parameters: folderid, folderowner, foldername, parentfolderid
            var fileSystemClient = await _dataLakeClientService.GetDataLakeFileSystemClient();
            var directoryClient = fileSystemClient.GetDirectoryClient(folder.fullPathFromRoot);

            // Deleting a folder means deleting all files underneath,
            // We need to mark these files as 'isdeleted' for indexer
            //folder = await myDataService.GetFolderStructure(folder, currentUser, false);

            await MarkChildFilesForDeletion(fileSystemClient, folder, currentUser);

            return directoryClient;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"DeleteAllFilesUnderneath folder: {folder.fullPathFromRoot} user: {currentUser.DisplayName} FAILED.");
            throw;
        }
    }

    protected async Task MarkChildFilesForDeletion(DataLakeFileSystemClient fileSystemClient, Folder folder, Microsoft.Graph.User currentUser)
    {
        if (folder != null)
        {
            try
            {
                var directoryClient = fileSystemClient.GetDirectoryClient(folder.fullPathFromRoot);
                foreach (FileMetaData file in folder.AllFiles)
                {
                    var fileClient = directoryClient.GetFileClient(file.filename);
                    await DeleteFileClient(fileClient, file, currentUser);
                }

                // Iterate down sub folders
                foreach (Folder subFolder in folder.SubFolders)
                {
                    await MarkChildFilesForDeletion(fileSystemClient, subFolder, currentUser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"MarkChildFilesForDeletion folder: {folder.fullPathFromRoot} user: {currentUser.DisplayName} FAILED.");
                throw;
            }
        }
    }

    protected void MarkFileForDeletion(DataLakeFileClient fileClient, FileMetaData file, Microsoft.Graph.User currentUser)
    {
        try
        {
            // Before deleting a file, we need to update timestamp so indexer can remove
            file.lastmodifiedts = DateTime.UtcNow;
            file.lastmodifiedby = currentUser.Id;
            file.isdeleted = "true";

            fileClient.SetMetadata(file.GenerateMetadata());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"MarkFileForDeletion file: {file.folderpath}/{file.filename} user: {currentUser.DisplayName} FAILED.");
            throw;
        }
    }

    protected async Task<bool> DeleteFileClient(DataLakeFileClient fileClient, FileMetaData file, Microsoft.Graph.User currentUser)
    {
        try
        {
            MarkFileForDeletion(fileClient, file, currentUser);

            var response = await fileClient.DeleteAsync();
            if (response.Status == 200)
            {
                //await _cognitiveSearchService.DeleteDocumentFromIndex(file.fileid);
            }

            var status = (response.Status == 200) ? "SUCCEEDED" : "FAILED";
            _logger.LogDebug($"Delete file: {file.folderpath}/{file.filename} user: {currentUser.DisplayName} {status}.");

            return response.Status == 200;
        }
        catch (Exception ex)
        {
            // Eat this excpetion, parent will handle!
            _logger.LogError(ex, $"Delete file: {file.folderpath}/{file.filename} user: {currentUser.DisplayName} FAILED.");
            throw;
        }
    }

    public async Task<bool> DeleteStorageBlob(FileMetaData file, string project, string containerName, Microsoft.Graph.User currentUser)
    {
        try
        {
            var connectionString = await _dataRetrievalService.GetProjectConnectionString(project.ToLower());
            return await CloudStorageAccount.Parse(connectionString)
                .CreateCloudBlobClient()
                .GetContainerReference(containerName)
                .GetBlockBlobReference(file.name)
                .DeleteIfExistsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete blob : {Filename} owned by: {DisplayName} FAILED", file.name, currentUser.DisplayName);   
            throw;
        }
    }
}