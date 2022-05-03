Function Add-Folder {  
    [cmdletbinding()]
    param (
        [Parameter(Mandatory = $true)][string]$Folder
    )

    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
  
    $url ="https://adb-5417680453087351.11.azuredatabricks.net/api/2.0/workspace/mkdirs"


    $Headers = @{
      Authorization = "Bearer $Token"
      "Content-Type" = "application/json"
    }


    $Body = @{"path"= $Folder}

    $BodyText = $Body | ConvertTo-Json -Depth 10


    Try {

         Invoke-RestMethod -Uri $url -Body $BodyText -Method 'POST' -Headers $Headers
    }
    Catch {
        if ($_.Exception.Response -eq $null) {
            throw $_.Exception.Message
        } else {
            if ($_.Exception.Response.StatusCode.value__ -eq 409){
                Write-Warning "Folder alrady exists"
            }
            else {
                throw $_.ErrorDetails.Message
            }    
        }  
    }


}