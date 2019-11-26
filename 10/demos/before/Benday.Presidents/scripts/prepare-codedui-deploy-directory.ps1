param(
    [Parameter(Mandatory=$true)]
    $buildConfiguration, 
    [Parameter(Mandatory=$true)]
    $buildArtifactsStagingDirectory, 
    [Parameter(Mandatory=$true)]
    $buildSourcesDirectory, 
    [Parameter(Mandatory=$true)]
    $toPath)

Write-Output "Preparing deployment directory for coded ui tests."
Write-Output "buildConfiguration: '$buildConfiguration'"
Write-Output "buildSourcesDirectory: '$buildSourcesDirectory'"
Write-Output "buildStagingDirectory: '$buildArtifactsStagingDirectory'"
Write-Output "toPath: '$toPath'"

if(!(Test-Path -Path $toPath)) 
{
    New-Item -ItemType directory -Path $toPath
}


Copy-Item "$buildArtifactsStagingDirectory\Benday.Presidents\Benday.Presidents.CodedUiTests\bin\$buildConfiguration\*codeduitests*.*" $toPath

Copy-Item "$buildSourcesDirectory\Benday.Presidents\scripts\set-environment-variable.ps1" $toPath