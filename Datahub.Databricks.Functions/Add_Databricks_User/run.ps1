# Input bindings are passed in via param block.
param($Timer)

# Get the current universal time in the default string format.
$currentUTCtime = (Get-Date).ToUniversalTime()
#sql server connection details




Import-Module Az.Sql

#Query the user table for users to be added
$SQLQuery1 = "SELECT a.*,b.PROJECT_ACRONYM_CD FROM DBO.ACCESS_REQUESTS a INNER JOIN dbo.Projects b ON a.PROJECT_ID = b.PROJECT_ID WHERE DATABRICKS = 1 AND COMPLETION_DT is NULL;"
$SQLQuery1Result = Invoke-Sqlcmd -query $SQLQuery1 -ServerInstance $SQLInstance -Database $SQLDatabase -Username $SQLUsername -Password $SQLPassword

#for each user record to be added
foreach ($UserRow in $SQLQuery1Result)
{
    #extract the username and request id
    $UserName = $UserRow.User_Name
    $RequestID = $UserRow.Request_ID
    $GroupName = $UserRow.PROJECT_ACRONYM_CD + "-WRITE-GRP"
    Write-Host $UserName
    try
    {
        #add user
        $strBody = Add-user -Username $UserName

        Write-Host $strBody "dfdsfdafdasdsfdasfdsfdsafds"

        #add user to the group
        Add-User-To-Group -User $UserName -Group $GroupName

        #update the record with the completion timestamp
        $SQLQuery2 = "UPDATE dbo.Access_Requests SET COMPLETION_DT  = CURRENT_TIMESTAMP WHERE Request_ID = "  + $RequestID
        Invoke-Sqlcmd -query $SQLQuery2 -ServerInstance $SQLInstance -Database $SQLDatabase -Username $SQLUsername -Password $SQLPassword

    
        Write-Host $UserName " has been successfully added to the group "  $GroupName

    }
    catch
    {
        Write-Output "Encoutered an error"
    }
}

# The 'IsPastDue' property is 'true' when the current function invocation is later than scheduled.
if ($Timer.IsPastDue) {
    Write-Host "PowerShell timer is running late!"
}

# Write an information log with the current time.
Write-Host "PowerShell timer trigger function ran! TIME: $currentUTCtime"
