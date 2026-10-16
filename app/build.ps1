# Restores NuGet packages and builds Grimoire for one or more platforms.
# Usage: .\build.ps1 [-Configuration Debug|Release] [-Platform x86,x64]
param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug',
    # A string array, but -File passes "x86,x64" as one string, so it is split below.
    [string[]]$Platform = @('x86', 'x64')
)

$ErrorActionPreference = 'Stop'
$solution = Join-Path $PSScriptRoot 'GrimliteRev.sln'

$Platform = @($Platform | ForEach-Object { $_ -split ',' } | ForEach-Object { $_.Trim() } | Where-Object { $_ })
foreach ($p in $Platform) {
    if ($p -notin 'x86', 'x64') { throw "Unknown platform '$p'. Use x86, x64 or x86,x64." }
}

# A running Grimoire.exe locks the output file, and MSBuild only fails after ten copy retries.
foreach ($p in $Platform) {
    $exe = Join-Path $PSScriptRoot "bin\$p\$Configuration\Grimoire.exe"
    $running = Get-Process -Name Grimoire -ErrorAction SilentlyContinue | Where-Object { $_.Path -eq $exe }
    if ($running) {
        Write-Host "Grimoire is running from $exe (PID $($running.Id -join ', ')). Close it, then build again." -ForegroundColor Red
        exit 1
    }
}

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
