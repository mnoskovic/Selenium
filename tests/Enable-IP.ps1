param
(
    [parameter()]
    $subscriptionId = "c3ac1123-f7fb-449a-aa57-11bba25f92c6", #TODO: 3PAGER SUBSCRIPTION ID

    [parameter()]
    $networkSecurityGroup = "selNsg", #TODO: 3PAGER NETWORK SECURITY GROUP

    [parameter()]
    $rulePrefix = "CUSTOM",

    [parameter()]
    $startPriority = 1000,

    [parameter()]
    $port = 80, #80 for tests & UI - 3389 FOR RDP

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
    Write-Host "Login to azure using your MSDN/Avanade/Accenture/BiBerk credentials in order to access azure resources"

    Login-AzureRmAccount | Out-Null

    Write-Host "Selecting subscription..."

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
    
   
    $current = $rules | ?{$_.Name -eq $addressName -or ($_.SourceAddressPrefix -eq $addressRange -and $_.DestinationPortRange -eq $port) } | Select-Object -First 1
    if ($current)
    {
        Write-Host "$addressRange already added to: $($group.ResourceGroupName) - $($group.Name)"
    }
    else
    {
        $priority = $startPriority
        $lastPriority = $rules | ?{$_.Name.StartsWith($rulePrefix) } | Sort-Object -Property Priority -Descending | Select-Object -Last 1 -Property Priority  
        if ($lastPriority -ne $null)
        {
            $priority = $lastPriority.Priority + 1
        }
        #$priority

        Write-Host "$addressRange adding to: $($group.ResourceGroupName) - $($group.Name)"
    
        Add-AzureRmNetworkSecurityRuleConfig -NetworkSecurityGroup $group `
            -Name $addressName -Protocol TCP `
            -SourcePortRange * -DestinationPortRange $port -SourceAddressPrefix $addressRange -DestinationAddressPrefix * `
            -Direction Inbound -Priority $priority -Access Allow -Verbose | Set-AzureRmNetworkSecurityGroup | Out-Null
    }
}

Write-Host "Done"


if ($local.IsPresent)
{
    pause
} 