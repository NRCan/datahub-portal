Function Get-Groups
{ 
    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12
 
 
    $Headers = @{
      Authorization = "Bearer $Token"
      "Content-Type" = "application/json"
    }
    
   $url = "https://adb-5417680453087351.11.azuredatabricks.net/api/2.0/groups/list" 

   $Groups = Invoke-RestMethod -Method Get -Uri $url -Headers $Headers

    Return $Groups.group_names
}