param(
    [Parameter(Mandatory=$true)]
    $envVarKey, 
    [Parameter(Mandatory=$true)]
    $envVarValue)

Write-Output "Setting user environment variable '$envVarKey' to value '$envVarValue'."

[Environment]::SetEnvironmentVariable("$envVarKey", "$envVarValue", "User")