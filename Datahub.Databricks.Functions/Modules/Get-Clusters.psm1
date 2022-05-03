
Function Get-Clusters 
{ 

    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12


    $Token = "dapi6bfcf69d1866427bffe163a9a8ed6ef5"
    $url = "https://adb-5417680453087351.11.azuredatabricks.net/api/2.0/clusters/list" 


    $Headers = @{
      Authorization = "Bearer $Token"
      "Content-Type" = "application/json"
    }

    Try {
        $Clusters = Invoke-RestMethod -Method Get -Uri $url -Headers $Headers
    }
    Catch {
        Write-Error "StatusCode:" $_.Exception.Response.StatusCode.value__ 
        Write-Error "StatusDescription:" $_.Exception.Response.StatusDescription
        Write-Error $_.ErrorDetails.Message
    }

    Return $Clusters.clusters

}