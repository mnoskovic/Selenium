powershell.exe -ExecutionPolicy bypass -Command "Start-Process powershell -Verb runAs -ArgumentList %~dp0run.ps1"

exit %errorlevel%