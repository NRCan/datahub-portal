using namespace System.Net

# Input bindings are passed in via param block.
param($Request, $TriggerMetadata)

# Write to the Azure Functions log stream.
Write-Host "PowerShell HTTP trigger function processed a request."

# Interact with query parameters or the body of the request.
$name = $Request.Query.Name
if (-not $name) {
    $name = $Request.Body.Name
}

$resourceURI = "https://database.windows.net/"
$tokenAuthURI = $env:MSI_ENDPOINT + "?resource=$resourceURI&api-version=2017-09-01"
$tokenResponse = Invoke-RestMethod -Method Get -Headers @{"Secret"="$env:MSI_SECRET"} -Uri $tokenAuthURI
$accessToken = $tokenResponse.access_token
 
$SqlConnection = New-Object System.Data.SqlClient.SqlConnection
$SqlConnection.ConnectionString = "Data Source =dh-portal-sql-dev.database.windows.net; Initial Catalog = dh-portal-projectdb"
$SqlConnection.AccessToken = $AccessToken

try
{
    $SqlConnection.Open()

    $SqlCmd = New-Object System.Data.SqlClient.SqlCommand
    $SqlCmd.CommandText = "SELECT a.*,b.PROJECT_ACRONYM_CD FROM DBO.ACCESS_REQUESTS a INNER JOIN dbo.Projects b ON a.PROJECT_ID = b.PROJECT_ID;"
    $SqlCmd.Connection = $SqlConnection
    $SqlAdapter = New-Object System.Data.SqlClient.SqlDataAdapter
    $SqlAdapter.SelectCommand = $SqlCmd
    $DataSet = New-Object System.Data.DataSet
    $SqlAdapter.Fill($DataSet) | Out-Null

    write-host $Dataset.Tables[0].Rows[1].item(1)
    
    write-host "hello"

    foreach ($Row in $DataSet.Tables[0].Rows)
    { 
        write-host "value is : $($Row[1])"
    }

    $SqlConnection.Close()
}
catch
{
  Write-Host "An error occurred with the connection to database"
} 


$body = "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."

if ($name) {
    $body = "Hello, $name. This HTTP triggered function executed successfully."
}

# Associate values to output bindings by calling 'Push-OutputBinding'.
Push-OutputBinding -Name Response -Value ([HttpResponseContext]@{
    StatusCode = [HttpStatusCode]::OK
    Body = $body
})
