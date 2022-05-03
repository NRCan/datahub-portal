Function Get-Folder-ID

{

    [cmdletbinding()]

    param ([parameter(Mandatory=$true)][string]$FolderName)

    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12
 
        $url = "https://adb-5417680453087351.11.azuredatabricks.net/api/2.0/workspace/list?path=/" 

        $Headers = @{
          Authorization = "Bearer $Token"
          "Content-Type" = "application/json"
        }

     $Response = Invoke-RestMethod -Method Get -Uri $url -Headers $Headers

 
     Return (($Response.objects | Where-Object {$_.path -eq "/" + $FolderName}).object_id)
 


 }

