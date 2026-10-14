# Restores NuGet packages and builds Grimoire for one or more platforms.
# Usage: .\build.ps1 [-Configuration Debug|Release] [-Platform x86,x64]
param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug',
    [ValidateSet('x86', 'x64')]
    [string[]]$Platform = @('x86', 'x64')
)

$ErrorActionPreference = 'Stop'
$solution = Join-Path $PSScriptRoot 'Project Grimlite.sln'

$vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
if (-not (Test-Path $vswhere)) { throw "vswhere.exe not found at $vswhere. Install Visual Studio or its Build Tools." }
$msbuild = & $vswhere -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
if (-not $msbuild) { throw 'MSBuild.exe not found. Install the MSBuild component of Visual Studio.' }

& $msbuild $solution /t:Restore /p:RestorePackagesConfig=true /nologo /v:m
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

foreach ($p in $Platform) {
    Write-Host "=== Building $Configuration|$p"
    & $msbuild $solution /t:Build /p:Configuration=$Configuration /p:Platform=$p /m /nologo /v:m
    if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
}
