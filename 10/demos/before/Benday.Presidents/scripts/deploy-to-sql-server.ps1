param(
	[Parameter(Mandatory=$true)]
    $dacpacPath,
	[Parameter(Mandatory=$true)]
    $connectionString)

& 'C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\120\sqlpackage.exe' /action:Publish /sourceFile:"$dacpacPath" /targetconnectionstring:"$connectionString"