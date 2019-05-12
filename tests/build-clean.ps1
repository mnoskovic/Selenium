cd $PSScriptRoot

cls

Get-ChildItem -Directory -Filter "bin" -Recurse 
Get-ChildItem -Directory -Filter "bin" -Recurse | Remove-Item -Force -Recurse

Get-ChildItem -Directory -Filter "obj" -Recurse 
Get-ChildItem -Directory -Filter "obj" -Recurse | Remove-Item -Force -Recurse

Get-ChildItem -Directory -Filter "packages" -Recurse 
Get-ChildItem -Directory -Filter "packages" -Recurse | Remove-Item -Force -Recurse

Get-ChildItem -Directory -Filter "node_modules" -Recurse 
Get-ChildItem -Directory -Filter "node_modules" -Recurse | Remove-Item -Force -Recurse

Get-ChildItem -File -Filter "*.exe" -Recurse 
Get-ChildItem -File -Filter "*.exe" -Recurse | Remove-Item -Force -Recurse

