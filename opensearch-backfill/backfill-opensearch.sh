#!/bin/bash

# Backfills OpenSearch with data from DynamoDB
# Runs the associated backfill-opensearch.py python script
# See: https://docs.amplify.aws/cli/graphql/troubleshooting/#backfill-opensearch-index-from-dynamodb-table

# Comment these lines to re-enable the script
echo "This script has already been run - and should not be needed again."
echo "If you need to run this script against fresh data, and a fresh Open Search index,"
echo "get new values for TABLE_NAME, LAMBDA_ARN, TABLE_STREAM_ARN. These are available"
echo "in the AWS Console (take a look at the dynamodb table and its stream settings)."
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
   TABLE_NAME="YobolHealthCentreFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev"
   LAMBDA_ARN="arn:aws:lambda:ap-southeast-1:251687087743:function:amplify-undpcambodiaplatf-OpenSearchStreamingLambd-clVLE6mtAVYe"
   TABLE_STREAM_ARN="arn:aws:dynamodb:ap-southeast-1:251687087743:table/YobolHealthCentreFeedback-n26gdt6xnfdxbj7sar6xeqdoju-dev/stream/2023-07-26T15:22:45.042"
fi

# production parameters
if [[ "$ENVIRO" == 'prod' ]]; then
   TABLE_NAME="YobolHealthCentreFeedback-rgtqen7offgm7dxbxvnapczgmy-production"
   LAMBDA_ARN="arn:aws:lambda:ap-southeast-1:251687087743:function:amplify-undpcambodiaplatf-OpenSearchStreamingLambd-NgxL4oNGnKPX"
   TABLE_STREAM_ARN="arn:aws:dynamodb:ap-southeast-1:251687087743:table/YobolHealthCentreFeedback-rgtqen7offgm7dxbxvnapczgmy-production/stream/2023-07-27T15:36:04.332"
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
