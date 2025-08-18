#!/bin/bash
set -euo pipefail

# Default values
VERSION_PROPERTY="SemVer"
FORCE=false
DRY_RUN=false

# Help function
show_help() {
    cat << EOF
Usage: $0 [OPTIONS]

Tags the current branch with the version from GitVersion.

OPTIONS:
    -p, --version-property PROPERTY   GitVersion property to use (default: SemVer)
                                     Valid values: SemVer, MajorMinorPatch, FullSemVer, AssemblySemVer
    -f, --force                      Force tag creation even if tag already exists
    -d, --dry-run                    Show what would be tagged without creating the tag
    -h, --help                       Show this help message

EXAMPLES:
    $0                              Creates a tag using SemVer version
    $0 -p MajorMinorPatch -f        Creates a tag using MajorMinorPatch and forces creation
    $0 --dry-run                    Shows what would be tagged without creating the tag
EOF
}

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        -p|--version-property)
            VERSION_PROPERTY="$2"
            shift 2
            ;;
        -f|--force)
            FORCE=true
            shift
            ;;
        -d|--dry-run)
            DRY_RUN=true
            shift
            ;;
        -h|--help)
            show_help
            exit 0
            ;;
        *)
            echo "Unknown option: $1"
            show_help
            exit 1
            ;;
    esac
done

# Validate version property
case $VERSION_PROPERTY in
    SemVer|MajorMinorPatch|FullSemVer|AssemblySemVer)
        ;;
    *)
        echo "Error: Invalid version property '$VERSION_PROPERTY'"
        echo "Valid values: SemVer, MajorMinorPatch, FullSemVer, AssemblySemVer"
        exit 1
        ;;
esac

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Check if we're in a git repository
if ! git rev-parse --git-dir > /dev/null 2>&1; then
    echo -e "${RED}Error: Not in a git repository. Please run this script from the root of your git repository.${NC}"
    exit 1
fi

# Check if dotnet CLI is available
if ! command -v dotnet &> /dev/null; then
    echo -e "${RED}Error: dotnet CLI is not available. Please install .NET SDK.${NC}"
    exit 1
fi

# Get version from GitVersion
echo -e "${BLUE}Getting version from GitVersion...${NC}"
if ! gitversion_output=$(dotnet gitversion 2>/dev/null); then
    echo -e "${RED}Error: Failed to get version from GitVersion${NC}"
    exit 1
fi

# Parse JSON output to get the specific version property
version=$(echo "$gitversion_output" | grep "\"$VERSION_PROPERTY\"" | cut -d'"' -f4)
if [[ -z "$version" ]]; then
    echo -e "${RED}Error: Could not get version property '$VERSION_PROPERTY' from GitVersion output${NC}"
    exit 1
fi

# Get additional info for display
branch_name=$(echo "$gitversion_output" | grep '"BranchName"' | cut -d'"' -f4)
commit_sha=$(echo "$gitversion_output" | grep '"Sha"' | cut -d'"' -f4)
short_sha=$(echo "$gitversion_output" | grep '"ShortSha"' | cut -d'"' -f4)

tag_name="v$version"

echo -e "${GREEN}Current branch: $branch_name${NC}"
echo -e "${GREEN}Current commit: $commit_sha${NC}"
echo -e "${GREEN}Version ($VERSION_PROPERTY): $version${NC}"
echo -e "${GREEN}Tag name: $tag_name${NC}"

# Check if tag already exists
if git tag -l "$tag_name" | grep -q "^$tag_name$"; then
    if [[ "$FORCE" != true ]]; then
        echo -e "${YELLOW}Warning: Tag '$tag_name' already exists. Use --force to override or choose a different version property.${NC}"
        exit 1
    fi
fi

if [[ "$DRY_RUN" == true ]]; then
    echo -e "${YELLOW}DRY RUN: Would create tag '$tag_name' on commit $short_sha${NC}"
    exit 0
fi

# Create the tag
echo -e "${BLUE}Creating tag '$tag_name'...${NC}"
if git tag -l "$tag_name" | grep -q "^$tag_name$" && [[ "$FORCE" == true ]]; then
    git tag -d "$tag_name"
    echo -e "${YELLOW}Deleted existing local tag '$tag_name'${NC}"
fi

if git tag -a "$tag_name" -m "Version $version"; then
    echo -e "${GREEN}Successfully created tag '$tag_name'${NC}"
else
    echo -e "${RED}Error: Failed to create tag${NC}"
    exit 1
fi

# Ask if user wants to push the tag
read -p "Do you want to push the tag to origin? (y/N): " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]; then
    echo -e "${BLUE}Pushing tag to origin...${NC}"
    
    # Delete remote tag if it exists and we're forcing
    if git ls-remote --tags origin | grep -q "refs/tags/$tag_name$" && [[ "$FORCE" == true ]]; then
        git push origin ":refs/tags/$tag_name" || echo -e "${YELLOW}Note: Remote tag might not have existed${NC}"
    fi
    
    if git push origin "$tag_name"; then
        echo -e "${GREEN}Successfully pushed tag '$tag_name' to origin${NC}"
    else
        echo -e "${RED}Error: Failed to push tag to origin${NC}"
        exit 1
    fi
fi

echo -e "${GREEN}Tag creation completed successfully!${NC}"
