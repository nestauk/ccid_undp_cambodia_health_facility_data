#!/bin/bash

# Backfills OpenSearch with data from DynamoDB
# Runs the associated backfill-opensearch.py python script
# See: https://docs.amplify.aws/cli/graphql/troubleshooting/#backfill-opensearch-index-from-dynamodb-table

# Remove these lines to re-enable the script
echo "This script has already been run - and should not be needed again."
exit 0

# Start
echo "Backfilling data from DynamoDB to OpenSearch..."
echo

# parameters
ENVIRO=$1

if [[ -z "$ENVIRO" ]]; then
   echo "Please provide environment as the first argument. Options are: dev, prod"
   exit 1
fi

# defaults
REGION="ap-southeast-1"

# dev parameters
if [[ "$ENVIRO" == 'dev' ]]; then
   TABLE_NAME="SortableSemanticFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev"
   LAMBDA_ARN="arn:aws:lambda:ap-southeast-1:251687087743:function:amplify-undpcambodiaplatf-OpenSearchStreamingLambd-clVLE6mtAVYe"
   TABLE_STREAM_ARN="arn:aws:dynamodb:ap-southeast-1:251687087743:table/SortableSemanticFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev/stream/2023-06-21T09:59:59.473"
fi

# production parameters
if [[ "$ENVIRO" == 'prod' ]]; then
   TABLE_NAME="SortableSemanticFeedback-rgtqen7offgm7dxbxvnapczgmy-production"
   LAMBDA_ARN="arn:aws:lambda:ap-southeast-1:251687087743:function:amplify-undpcambodiaplatf-OpenSearchStreamingLambd-NgxL4oNGnKPX"
   TABLE_STREAM_ARN="arn:aws:dynamodb:ap-southeast-1:251687087743:table/SortableSemanticFeedback-rgtqen7offgm7dxbxvnapczgmy-production/stream/2023-06-21T23:27:10.179"
fi

if [[ -z "$TABLE_NAME" ]]; then
   echo "Parameters not known for environment: $ENVIRO"
   exit 1
fi

echo "Environment:      $ENVIRO"
echo "Region:           $REGION"
echo "Table name:       $TABLE_NAME"
echo "Lambda ARN:       $LAMBDA_ARN"
echo "Table stream ARN: $TABLE_STREAM_ARN"
echo

python3 ./backfill-opensearch.py \
   --rn $REGION \
   --tn $TABLE_NAME \
   --lf $LAMBDA_ARN \
   --esarn $TABLE_STREAM_ARN
