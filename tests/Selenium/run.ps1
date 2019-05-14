
$dir = "$PWD\bin"
Write-Host $dir

if(-NOT (Test-Path $dir))
{
    md $dir
}

$selenium="$dir\selenium-standalone-server.jar"

if (-NOT (Test-Path $selenium))
{
    (New-Object Net.WebClient).DownloadFile('https://goo.gl/s4o9Vx', $selenium)
}

$root = "$PWD\..\packages"

$root | Get-ChildItem -Include "*.exe" -Recurse | %{

    copy-item -Path $_.FullName -Destination $dir -Force

}

Start-Process cmd -ArgumentList @('/C java', "-Dwebdriver.chrome.driver=$dir\chromedriver.exe", '-jar', $selenium);