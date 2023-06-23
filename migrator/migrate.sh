#!/bin/bash

# migrates from a Semantic data table to Sortable Semantic data table
# un-comment as appropriate

# dev tables
# FROM_TABLE=SemanticFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev
# TO_TABLE=SortableSemanticFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev

# prod read, dev write
# FROM_TABLE=SemanticFeedback-rgtqen7offgm7dxbxvnapczgmy-production
# TO_TABLE=SortableSemanticFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev

# prod read, prod write
# FROM_TABLE=SemanticFeedback-rgtqen7offgm7dxbxvnapczgmy-production
# TO_TABLE=SortableSemanticFeedback-rgtqen7offgm7dxbxvnapczgmy-production

dotnet run --project YobolMigrator/YobolMigrator.csproj $FROM_TABLE $TO_TABLE false
