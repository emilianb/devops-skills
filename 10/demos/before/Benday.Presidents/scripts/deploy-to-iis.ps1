param($pathToFiles, $siteName, $appName, $appPoolName)

New-WebApplication -Name $appName -ApplicationPool $appPoolName -Site "$siteName" -PhysicalPath $pathToFiles -Force