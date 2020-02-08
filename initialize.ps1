$loc = Get-Location
$oldName = Read-Host 'Enter the old project name (leave blank for default)'
$newName = Read-Host 'Enter the new project name'
if ([string]::IsNullOrWhiteSpace($oldName)) { $oldName = 'SolutionName' } 
Start-Process -FilePath "etc/ProjectInitializer/TemplateConfig.exe" -ArgumentList $loc, $oldName, $newName

