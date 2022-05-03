Function Add-User-To-Group
{
    [cmdletbinding()]
    param (
        [parameter(Mandatory=$true)][string]$User,
        [parameter(Mandatory=$true)][string]$Group
    )

    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

    $url = "https://adb-5417680453087351.11.azuredatabricks.net/api/2.0/groups/add-member" 
    
    $Headers = @{
      Authorization = "Bearer $Token"
      "Content-Type" = "application/json"
    }


    $body = '{"user_name": "' + $User + '", "parent_name": "' + $Group.ToUpper() + '"   }'

      
    Invoke-RestMethod -Method Post -Body $body -Uri $url -Headers $Headers
        
    Write-Verbose "User $UserName added to the group $Parent"
  
    

}