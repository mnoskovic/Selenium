param
(
    [parameter()]
    $subscriptionId = "c3ac1123-f7fb-449a-aa57-11bba25f92c6", #TODO: 3PAGER SUBSCRIPTION ID

    [parameter()]
    $networkSecurityGroup = "selNsg", #TODO: 3PAGER NETWORK SECURITY GROUP

    [parameter()]
    $rulePrefix = "CUSTOM",

    [parameter()]
    $port = 80, #3389 FOR RDP

    [Parameter()]
    [switch] $local
)

trap
{
    Write-Error $_
    
    if ($local.IsPresent)
    {
        pause
        exit
    }
}

if ($local.IsPresent)
{
    Write-Information "Login to azure using your MSDN/Avanade/Accenture/BiBerk credentials in order to access azure resources"

    Login-AzureRmAccount | Out-Null

    Write-Information "Selecting subscription..."

    Select-AzureRmSubscription -SubscriptionId $subscriptionId | Out-Null
}


$addressRange = Invoke-RestMethod http://ipinfo.io/json | Select -exp ip
Write-Host "Current IP: $addressRange"

$groups = Get-AzureRmNetworkSecurityGroup | ?{ $_.Name -eq $networkSecurityGroup }
$groups | %{ 


    $group = $_
    
    $addressName = $rulePrefix + "_" + $port + "_" + $addressRange
    #$addressName
   
    $rules = Get-AzureRmNetworkSecurityRuleConfig -NetworkSecurityGroup $group
    #$rules
    
    $current = $rules | ?{$_.Name -eq $addressName }
    if ($current)
    {
        Write-Host "Removing rule $($current.Name)..."
        Remove-AzureRmNetworkSecurityRuleConfig -NetworkSecurityGroup $group -Name $addressName -Verbose | Set-AzureRmNetworkSecurityGroup | Out-Null
    }
    else
    {
        $existing = $rules | ?{$_.SourceAddressPrefix -eq $addressRange -and $_.DestinationPortRange -eq $port } | Select-Object -First 1
        if ($existing)
        {
            Write-Warning "There is another rule $($existing.Name) that enables address $addressRange to accesss port $port"
        }
        else
        {
            Write-Host "Rule to be removed not found"
        }
    }
}

Write-Host "Done"


if ($local.IsPresent)
{
    pause
} 