using namespace System.Net

# Input bindings are passed in via param block.
param($Request, $TriggerMetadata)

# Write to the Azure Functions log stream.
Write-Host "PowerShell HTTP trigger function processed a request."

# Interact with query parameters or the body of the request.
$ProjectID = $Request.Query.ProjectID
if (-not $ProjectID) {
    $ProjectID = $Request.Body.ProjectID
}

Import-Module Az.Sql

$body = "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."

if ($ProjectID) {

    #query the db to get the project fields
    $SQLQuery1 = "SELECT * FROM DBO.PROJECTS WHERE PROJECT_ID = " + $ProjectID
    $ProjectRecord = Invoke-Sqlcmd -query $SQLQuery1 -ServerInstance $SQLInstance -Database $SQLDatabase -Username $SQLUsername -Password $SQLPassword

    $ProjectAcronym = $ProjectRecord.Project_Acronym_CD
    $Sector = $ProjectRecord.Sector_Name
    $Branch = $ProjectRecord.Branch_Name

    #setup values based on project acronym
    $ClusterName = 'Cluster-' + $ProjectAcronym
    $DeveloperGroupName = $ProjectAcronym + '-WRITE-GRP'
    $ReaderGroupName = $ProjectAcronym + '-READ-GRP'
    $FolderName = "/" + $ProjectAcronym

    #create folder and retrieve the folder id
    Add-Folder -Folder $FolderName
    $FolderID = Get-Folder-ID -FolderName $ProjectAcronym

    #create groups
    Add-group -GroupName $DeveloperGroupName
    Add-group -GroupName $ReaderGroupName
  
    #assign permission on folder
    Assign-Permission-On-Folder -FolderID $FolderID -ObjectType group -ObjectName  $DeveloperGroupName -Permission CAN_MANAGE
    Assign-Permission-On-Folder -FolderID $FolderID -ObjectType group -ObjectName  $ReaderGroupName -Permission CAN_READ

    #create cluster and reterive the cluster id. Add-Cluster returns 0 if cluster already exists with the same name
    $ClusterID = Add-Cluster -ClusterName $ClusterName -MinNumberOfWorkers 2 -MaxNumberOfWorkers 4   -SparkVersion "7.3.x-scala2.12"  -NodeType "Standard_D3_v2" -CustomTags @{ Project = $ProjectAcronym; Sector = $Sector; Branch = $Branch}

    if ($ClusterID -ne "0") {
        #assign permission on cluster
        Assign-User-Permission-On-Cluster -ClusterID $ClusterID -ObjectType group -ObjectName $DeveloperGroupName -Permission CAN_RESTART
    }

    #generate the url for the project
    $ProjectURL = $workspace + "/?o=" + $workspaceID + "#folder/" + $FolderID
 
    #update the project database with the url
    $SQLQuery2 = "UPDATE DBO.PROJECTS SET DATABRICKS_URL = '" + $ProjectURL + "' WHERE PROJECT_ID = " + $ProjectID
    Invoke-Sqlcmd -query $SQLQuery2 -ServerInstance $SQLInstance -Database $SQLDatabase -Username $SQLUsername -Password $SQLPassword

}


# Associate values to output bindings by calling 'Push-OutputBinding'.
Push-OutputBinding -Name Response -Value ([HttpResponseContext]@{
    StatusCode = [HttpStatusCode]::OK
    Body = $ProjectURL
})
