param(
    [Parameter(Mandatory=$true)]
    $buildConfiguration, 
    [Parameter(Mandatory=$true)]
    $buildArtifactsStagingDirectory, 
    [Parameter(Mandatory=$true)]
    $buildSourcesDirectory, 
    [Parameter(Mandatory=$true)]
    $toPath)

Write-Output "Preparing deployment directory for database deployment."
Write-Output "buildConfiguration: '$buildConfiguration'"
Write-Output "buildSourcesDirectory: '$buildSourcesDirectory'"
Write-Output "buildStagingDirectory: '$buildArtifactsStagingDirectory'"
Write-Output "toPath: '$toPath'"

if(!(Test-Path -Path $toPath)) 
{
    New-Item -ItemType directory -Path $toPath
}


Copy-Item "$buildArtifactsStagingDirectory\Benday.Presidents\Benday.Presidents.Database\bin\$buildConfiguration\*.dacpac" $toPath

Copy-Item "$buildSourcesDirectory\Benday.Presidents\scripts\deploy-to-sql-server.ps1" $toPath