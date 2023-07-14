#!/bin/bash

# prod table
# TABLE=SortableSemanticFeedback-rgtqen7offgm7dxbxvnapczgmy-production

# dev table
TABLE="SortableSemanticFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev"

# mode and cutoff date
MODE="ReIndexFriendlinessAndSpeed"
BEFORE="2023-07-14T18:13:26.363Z"
WRITE=false

dotnet run --project DataRepair/DataRepair.csproj $MODE $TABLE $WRITE $BEFORE
