using Datahub.Achievements.Models;
using Datahub.Core.Data;
using Datahub.Core.Services;
using Datahub.Portal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Datahub.Portal.Pages.Project.FileExplorer;

public partial class FileExplorer
{
    private async Task FetchStorageBlobsPageAsync()
    {
        _loading = true;
        StateHasChanged();
        
        var (folders, files, continuationToken) = await _projectDataRetrievalService.GetDfsPagesAsync(ProjectAcronym,
            ContainerName, _currentFolder, _continuationToken);

        _continuationToken = continuationToken;
        _files = files;
        _folders = folders;

        _loading = false;
        StateHasChanged();
    }

    
    private async Task HandleNewFolder(string newFolderName)
    {
        if (_folders.Contains(newFolderName))
            return;

        newFolderName = newFolderName
            .Replace("/", "")
            .Trim();

        _folders.Add($"{_currentFolder}{newFolderName}/");
        await _achievementService.AddOrIncrementTelemetryEvent(DatahubUserTelemetry.TelemetryEvents.UserCreateFolder);
    }
    
    private async Task HandleFileDelete(string fileName)
    {
        var message = string.Format(Localizer["Are you sure you want to delete file \"{0}\"?"], fileName);
        if (!await _jsRuntime.InvokeAsync<bool>("confirm", message))
            return;

        if (!await _projectDataRetrievalService.DeleteFileAsync(ProjectAcronym, ContainerName, JoinPath(_currentFolder, fileName)))
            return;

        _files?.RemoveAll(f => f.name.Equals(fileName, StringComparison.OrdinalIgnoreCase));

        _selectedItems = new HashSet<string> { _currentFolder };

        await _achievementService.AddOrIncrementTelemetryEvent(DatahubUserTelemetry.TelemetryEvents.UserDeleteFile);

    }

    private string JoinPath(string folder, string fileName)
    {
        var splitPath = (folder ?? "").Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();
        splitPath.Add(fileName);
        return string.Join("/", splitPath);
    }

    private async Task HandleFileItemDrop(string folder, string filename)
    {
        if (!string.IsNullOrWhiteSpace(folder) && !string.IsNullOrWhiteSpace(filename))
        {
            var oldFilename = (_currentFolder + filename).TrimStart('/');
            var newFilename = (folder + filename).TrimStart('/');

            if (await PreventOverwrite(newFilename))
                return;
            
            //await _dataUpdatingService.RenameStorageBlob(oldFilename, newFilename, ProjectAcronym, ContainerName);
            _files.RemoveAll(f => f.name == oldFilename);
        }
    }

    private async Task<(bool FileExists, bool AllowOverride)> VerifyOverwrite(string filePath)
    {
        if (!await _projectDataRetrievalService.FileExistsAsync(ProjectAcronym, ContainerName, filePath))
            return (false, true);

        var allowOverride = await _jsRuntime.InvokeAsync<bool>("confirm",
            string.Format(Localizer["File '{0}' already exists. Do you want to overwrite it?"], filePath));

        return (true, allowOverride);
    }
    private async Task HandleFileRename(string fileRename)
    {
        if (string.IsNullOrWhiteSpace(fileRename))
            return;

        var currentFileName = GetFileName(_selectedItem);

        var oldFileName = JoinPath(_currentFolder, currentFileName);
        var newFileName = JoinPath(_currentFolder, fileRename);

        var (fileExists, allowOverride) = await VerifyOverwrite(newFileName);
        if (!allowOverride)
            return;

        if (!await _projectDataRetrievalService.RenameFileAsync(ProjectAcronym, ContainerName, oldFileName, newFileName))
            return;

        if (fileExists)
        {
            _files.RemoveAll(f => f.name == fileRename);
        }

        var targetFile = _files.FirstOrDefault(f => f.name == currentFileName);
        if (targetFile is not null)
            targetFile.name = fileRename;
    }

    private async Task<bool> IfFileExistsInLocation(string filename)
    {
        return await _dataRetrievalService.StorageBlobExistsAsync(filename, ProjectAcronym, ContainerName);
    }

    private async Task<bool> PreventOverwrite(string filename)
    {
        if (await IfFileExistsInLocation(filename))
        {
            return !await _jsRuntime.InvokeAsync<bool>("confirm",
                string.Format(Localizer["File '{0}' already exists. Do you want to overwrite it?"], filename));
        }

        return false;
    }
    
    private async Task UploadFile(IBrowserFile browserFile, string folder)
    {
        if (browserFile == null)
            return;

        var newFilename = (folder + browserFile.Name).TrimStart('/');
        if (await PreventOverwrite(newFilename))
            return;

        var fileMetadata = new FileMetaData
        {
            id = Guid.NewGuid().ToString(),
            createdby = GraphUser.Mail,
            folderpath = folder,
            filename = (folder + browserFile.Name).TrimStart('/'),
            filesize = browserFile.Size.ToString(),
            uploadStatus = FileUploadStatus.SelectedToUpload,
            bytesToUpload = browserFile.Size,
            createdts = DateTime.UtcNow,
            lastmodifiedts = DateTime.UtcNow,
            BrowserFile = browserFile
        };

        //await _apiService.PopulateOtherMetadata(fileMetadata);
        _uploadingFiles.Add(fileMetadata);

        _ = InvokeAsync(async () =>
        {
            var succeeded = await _projectDataRetrievalService.UploadFileAsync(ProjectAcronym.ToLower(), ContainerName, fileMetadata, uploadedBytes =>
            {
                fileMetadata.uploadedBytes = uploadedBytes;
                _ = InvokeAsync(StateHasChanged);
            });

            _uploadingFiles.Remove(fileMetadata);
            if (folder == _currentFolder)
            {
                if (succeeded)
                {
                    if (1 == 0)
                    {
                        _files.RemoveAll(f => f.name == fileMetadata.name);
                    }
                    _files.Add(fileMetadata);
                }
            }

            StateHasChanged();
        });

        StateHasChanged();
    }

    private async Task HandleFileDownload(string filename)
    {   
        var selectedFile = _files?.FirstOrDefault(f => f.name == filename);
        //var uri = await _dataRetrievalService.DownloadFile(ContainerName, selectedFile, ProjectAcronym);
        var uri = await _projectDataRetrievalService.DownloadFileAsync(ProjectAcronym, ContainerName, filename);
        await _module.InvokeVoidAsync("downloadFile", uri.ToString());
    }
    
    
    private string GetDirectoryName(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !path.Contains("/"))
            return string.Empty;

        var lastIndex = path.TrimEnd('/').LastIndexOf("/", StringComparison.Ordinal);
        return lastIndex == -1 ? "/" : path[..lastIndex] + "/";
    }

    public static string GetFileName(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return string.Empty;

        var lastIndex = path.TrimEnd('/').LastIndexOf("/", StringComparison.Ordinal);
        return lastIndex == -1 ? path : path[(lastIndex + 1)..];
    }

    private async Task SetCurrentFolder(string folderName)
    {
        _currentFolder = folderName;
        _selectedItems = new HashSet<string> { folderName };
        await FetchStorageBlobsPageAsync();
    }

    private async Task UploadFiles(InputFileChangeEventArgs e)
    {
        await UploadFiles(e, _currentFolder);
    }

    private async Task UploadFiles(InputFileChangeEventArgs e, string folderName)
    {
        foreach (var browserFile in e.GetMultipleFiles())
        {
            await UploadFile(browserFile, folderName);
        }
    }

    private void HandleSearch(string newValue, KeyboardEventArgs ev)
    {
        _filterValue = newValue.Trim();
        StateHasChanged();
    }

    private void ResetSearch()
    {
        _filterValue = string.Empty;
    }
    
    
    private void HandleFileSelectionClick(string filename)
    {
        _selectedItems.RemoveWhere(i => i.EndsWith("/", StringComparison.InvariantCulture));
        
        if (_selectedItems.Contains(filename))
        {
            _selectedItems.Remove(filename);
        }
        else
        {
            _selectedItems.Add(filename);
        }
    }
}