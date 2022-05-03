
function CreateArray {
    [cmdletbinding()]
    param (
        [string]$Parent,
        [string[]]$Values
    )
    
    $ResArray = @()
    ForEach ($e in $Values) {
        $ResArray += @{"value"=$e}
    }

    return @{"$Parent"=$ResArray} 
}