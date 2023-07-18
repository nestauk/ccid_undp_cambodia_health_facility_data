#!/bin/bash

# prod table
# TABLE=SortableSemanticFeedback-rgtqen7offgm7dxbxvnapczgmy-production

# dev table
TABLE="SortableSemanticFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev"

# mode and cutoff date
MODE="ReIndexFriendlinessAndSpeed"
BEFORE="2023-07-14T18:13:26.363Z"
WRITE=false

echo "MODE:  $MODE"
echo "WRITE: $WRITE"
echo "TABLE: $TABLE"
echo
echo "Set WRITE to false for a dry run, true to enact the chosen repair."
echo "Set TABLE to the table name (there's a prod and dev table)."
echo "See comments in this script for more information."
echo

dotnet run --project DataRepair/DataRepair.csproj $MODE $TABLE $WRITE $BEFORE
