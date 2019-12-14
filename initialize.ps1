$loc = Get-Location
$projName = Read-Host 'Enter the new project name'
Start-Process -FilePath "etc/ProjectInitializer/TemplateConfig.exe" -ArgumentList $projName, $loc

