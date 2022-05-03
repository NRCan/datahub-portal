Function Add-Group
{

    [cmdletbinding()]
    param ([parameter(Mandatory=$true)][string]$GroupName)

    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12
 
    $url = "https://adb-5417680453087351.11.azuredatabricks.net/api/2.0/groups/create" 


    $Headers = @{
      Authorization = "Bearer $Token"
      "Content-Type" = "application/json"
    }

    $body = '{"group_name": "' + $GroupName + '"  }'

    
    $Groups = Get-Groups
    $GroupsUpper = $groups.ToUpper()

       
    if ($GroupsUpper.Contains($GroupName.ToUpper())) {

        write("Group alrady exists")
    }

    else
    
    {
        Invoke-RestMethod -Method Post -Body $body -Uri $url -Headers $Headers
    
        Write-Output "$GroupName has been created"
    }



}