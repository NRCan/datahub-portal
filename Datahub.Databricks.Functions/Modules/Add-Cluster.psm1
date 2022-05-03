
Function Add-Cluster {  
    [cmdletbinding()]
    param (
        [parameter(Mandatory = $false)][string]$ClusterName,
        [parameter(Mandatory = $false)][string]$SparkVersion,
        [parameter(Mandatory = $false)][string]$NodeType,
        [parameter(Mandatory = $false)][string]$DriverNodeType,
        [parameter(Mandatory = $false)][int]$MinNumberOfWorkers,
        [parameter(Mandatory = $false)][int]$MaxNumberOfWorkers,
        [parameter(Mandatory = $false)][int]$AutoTerminationMinutes,
        [parameter(Mandatory = $false)][hashtable]$CustomTags,
        [parameter(Mandatory = $false)][ValidateSet(2,3)] [string]$PythonVersion=3
    ) 
   
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
    
    $url = "https://adb-5417680453087351.11.azuredatabricks.net/api/2.0/clusters/create" 

    $Headers = @{
      Authorization = "Bearer $Token"
      "Content-Type" = "application/json"
    }
    
    
      if (( Get-Clusters | Where-Object {$_.cluster_name -eq $ClusterName})) {
            return "0"
        }
    
    $Body = @{}

    $Body += @{"cluster_name"=$ClusterName} 
   
     If (($PSBoundParameters.ContainsKey('SparkVersion')) -and ($SparkVersion)) { $Body['spark_version'] = $SparkVersion }
      If (($PSBoundParameters.ContainsKey('NodeType')) -and ($NodeType)) { $Body['node_type_id'] = $NodeType }
    If (($PSBoundParameters.ContainsKey('DriverNodeType')) -and ($DriverNodeType)) { $Body['driver_node_type_id'] = $DriverNodeType }
    
    If ($MinNumberOfWorkers -gt 0){
        If ($MinNumberOfWorkers -eq $MaxNumberOfWorkers){
            $Body['num_workers'] = $MinNumberOfWorkers
        }
        else {
            $Body['autoscale'] = @{"min_workers"=$MinNumberOfWorkers;"max_workers"=$MaxNumberOfWorkers}
        }
    }

    If (($PSBoundParameters.ContainsKey('CustomTags')) -and ($null -ne $CustomTags)) {
       $Body['custom_tags'] = $CustomTags
    }


    If ($AutoTerminationMinutes -gt 0) {$Body['autotermination_minutes'] = $AutoTerminationMinutes}

    $Body = $Body | ConvertTo-Json -Depth 10 
 
  $Response = Invoke-RestMethod -Method Post -Body $Body -Uri $url -Headers $Headers

   # write $Body

    

      return $Response.cluster_id
  #>
    
}
