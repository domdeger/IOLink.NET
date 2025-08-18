#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Tags the current branch with the version from GitVersion
.DESCRIPTION
    This script uses GitVersion to determine the current version and creates a git tag.
    It supports different version formats and includes safety checks.
.PARAMETER VersionProperty
    The GitVersion property to use for tagging (default: SemVer)
.PARAMETER Force
    Force tag creation even if tag already exists
.PARAMETER DryRun
    Show what would be tagged without actually creating the tag
.EXAMPLE
    .\tag-version.ps1
    Creates a tag using the SemVer version
.EXAMPLE
    .\tag-version.ps1 -VersionProperty "MajorMinorPatch" -Force
    Creates a tag using MajorMinorPatch version and forces creation
#>

param(
    [Parameter(HelpMessage = "GitVersion property to use for tagging")]
    [ValidateSet("SemVer", "MajorMinorPatch", "FullSemVer", "AssemblySemVer")]
    [string]$VersionProperty = "SemVer",
    
    [Parameter(HelpMessage = "Force tag creation even if tag already exists")]
    [switch]$Force,
    
    [Parameter(HelpMessage = "Show what would be tagged without creating the tag")]
    [switch]$DryRun
)

# Check if we're in a git repository
if (-not (Test-Path ".git") -and -not (git rev-parse --git-dir 2>$null)) {
    Write-Error "Not in a git repository. Please run this script from the root of your git repository."
    exit 1
}

# Check if GitVersion is available
try {
    $null = Get-Command "dotnet" -ErrorAction Stop
} catch {
    Write-Error "dotnet CLI is not available. Please install .NET SDK."
    exit 1
}

# Get version from GitVersion
Write-Host "Getting version from GitVersion..." -ForegroundColor Blue
try {
    $gitVersionOutput = dotnet gitversion | ConvertFrom-Json
    if ($LASTEXITCODE -ne 0) {
        throw "GitVersion failed with exit code $LASTEXITCODE"
    }
} catch {
    Write-Error "Failed to get version from GitVersion: $_"
    exit 1
}

$version = $gitVersionOutput.$VersionProperty
if (-not $version) {
    Write-Error "Could not get version property '$VersionProperty' from GitVersion output"
    exit 1
}

$tagName = "v$version"

Write-Host "Current branch: $($gitVersionOutput.BranchName)" -ForegroundColor Green
Write-Host "Current commit: $($gitVersionOutput.Sha)" -ForegroundColor Green
Write-Host "Version ($VersionProperty): $version" -ForegroundColor Green
Write-Host "Tag name: $tagName" -ForegroundColor Green

# Check if tag already exists
$tagExists = git tag -l $tagName
if ($tagExists -and -not $Force) {
    Write-Warning "Tag '$tagName' already exists. Use -Force to override or choose a different version property."
    exit 1
}

if ($DryRun) {
    Write-Host "DRY RUN: Would create tag '$tagName' on commit $($gitVersionOutput.ShortSha)" -ForegroundColor Yellow
    exit 0
}

# Create the tag
Write-Host "Creating tag '$tagName'..." -ForegroundColor Blue
try {
    if ($Force -and $tagExists) {
        git tag -d $tagName
        Write-Host "Deleted existing local tag '$tagName'" -ForegroundColor Yellow
    }
    
    git tag -a $tagName -m "Version $version"
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to create tag"
    }
    
    Write-Host "Successfully created tag '$tagName'" -ForegroundColor Green
    
    # Ask if user wants to push the tag
    $pushTag = Read-Host "Do you want to push the tag to origin? (y/N)"
    if ($pushTag -eq "y" -or $pushTag -eq "Y") {
        Write-Host "Pushing tag to origin..." -ForegroundColor Blue
        if ($Force -and $tagExists) {
            git push origin :refs/tags/$tagName  # Delete remote tag if it exists
        }
        git push origin $tagName
        if ($LASTEXITCODE -eq 0) {
            Write-Host "Successfully pushed tag '$tagName' to origin" -ForegroundColor Green
        } else {
            Write-Error "Failed to push tag to origin"
            exit 1
        }
    }
    
} catch {
    Write-Error "Failed to create tag: $_"
    exit 1
}

Write-Host "Tag creation completed successfully!" -ForegroundColor Green
