#!/bin/bash

ENV=$1
MODE=$2
WRITE=$3
PARAM=$4

# if ENV is not provided
if [[ -z "$ENV" ]]; then
   echo "Please provide environment as the first argument. Options are: dev, prod"
   exit 1
fi

# if MODE is not provided
if [[ -z "$MODE" ]]; then
   echo "Please provide mode as the second argument. Options are:"
   echo " - ReIndexFriendlinessAndSpeed"
   echo " - AddConflictResolutionValues"
   echo " - MigrateToYobolHealthCentreFeedback"
   exit 1
fi

# if WRITE is not provided
if [[ -z "$WRITE" ]]; then
    echo "Indicate write enabled: true or false in the third argument."
    exit 1
fi

# if ENV is "dev" then use the dev table, otherwise use the prod table
if [[ "$ENV" == 'prod' ]]; then
   # prod table: SortableSemanticFeedback-rgtqen7offgm7dxbxvnapczgmy-production
   TABLE="SortableSemanticFeedback-rgtqen7offgm7dxbxvnapczgmy-production"
else
   # dev table: SortableSemanticFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev
   TABLE="SortableSemanticFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev"
fi

# mode and cutoff date. modes:
# ReIndexFriendlinessAndSpeed = incremented friendliness and speed values (original data was 0-indexed)
# AddConflictResolutionValues = provide values for _lastChangedAt, _version, _deleted
# BEFORE="2023-07-14T18:13:26.363Z"
# MODE="AddConflictResolutionValues"
# WRITE=true

echo "MODE:  $MODE"
echo "WRITE: $WRITE"
echo "TABLE: $TABLE"
echo "PARAM: $PARAM"
echo
echo "Set WRITE to false for a dry run, true to enact the chosen repair."
echo "Set TABLE to the table name (there's a prod and dev table)."
echo "See comments in this script for more information."
echo

dotnet run --project DataRepair/DataRepair.csproj $MODE $TABLE $WRITE $PARAM
