
Function Assign-User-Permission-On-Cluster
 { 

   [cmdletbinding()]
    param ([Parameter(Mandatory = $true)][string]$ClusterID,
            [Parameter(Mandatory = $true)][string]$ObjectType,
            [Parameter(Mandatory = $true)][string]$ObjectName,
            [Parameter(Mandatory = $true)][string]$Permission)
          
  

    $url = "https://adb-5417680453087351.11.azuredatabricks.net/api/2.0/preview/permissions/clusters/$ClusterID/" 

    $Headers = @{
      Authorization = "Bearer $Token"
      "Content-Type" = "application/json"
     }

    $objectTypeNm = $ObjectType + "_name"

    $body = convertto-json @{ "access_control_list" =  @( @{ $objectTypeNm=$ObjectName; permission_level=$Permission}) }

     Invoke-RestMethod -Method Patch -Body $body  -Uri $url -Headers $Headers

 }

