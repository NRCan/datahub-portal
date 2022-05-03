Function Add-User
{
    [cmdletbinding()]
    param (
	[parameter(Mandatory=$true)][string]$Username,
        [parameter(Mandatory=$false)][string[]]$Entitlements
    )


[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12
 
$Headers = @{
  Authorization = "Bearer $Token"
  "Content-Type" = "application/json"
}
   

    $schema = @{"schemas"="urn:ietf:params:scim:schemas:core:2.0:User"}
    $entitlementsFormatted = (CreateArray "entitlements" $Entitlements)
    $usernameFormatted = @{"userName"=$Username}

    $url = "https://adb-5417680453087351.11.azuredatabricks.net/api/2.0/preview/scim/v2/Users" 

    $Body = ($schema + $entitlementsFormatted + $usernameFormatted) | ConvertTo-Json -Depth 10 

   

    Try {

         $Request = Invoke-RestMethod -Method Post -Body $Body -Uri $url -Headers $Headers -ContentType "application/scim+json"
    }
    Catch {
        if ($_.Exception.Response -eq $null) {
            throw $_.Exception.Message
        } else {
            if ($_.Exception.Response.StatusCode.value__ -eq 409){
                Write-Warning "User alrady exists"
            }
            else {
                throw $_.ErrorDetails.Message
            }    
        }  
    }


    write $Body
    
    

    return $Body
}