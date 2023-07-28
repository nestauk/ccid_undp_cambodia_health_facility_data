#!/bin/bash

# exit on error
set -e
set -o pipefail

usage() {
  cat << EOF
  duplicate-table.sh <first-table-name> <second-table-name>
EOF
}

# read arguments
TABLE_FROM=$1
TABLE_TO=$2

# defaults
REGION=ap-southeast-1

if [[ -z "$TABLE_FROM" ]]; then
    echo "Please provide the source table name as the first parameter."
    usage
    exit 1
fi

if [[ -z "$TABLE_TO" ]]; then
    echo "Please provide the target table name as the second parameter."
    usage
    exit 1
fi

echo "Source: $TABLE_FROM"
echo "Target: $TABLE_TO"
echo

# some temporary files
TEMP_PAYLOAD_FILE="temp-${TABLE_TO}-payload.json"
TEMP_WRITE_REPORT_FILE="temp-write-report.jsonl"

# clear out any old temporary data
rm -f $TEMP_PAYLOAD_FILE
rm -f $TEMP_WRITE_REPORT_FILE

read_and_write() {
    # read from source table
    echo "Reading data from $TABLE_FROM..."
    if [[ -z "$NEXT_TOKEN" ]]; then
        DATA=$(aws dynamodb scan --table-name "$TABLE_FROM" --max-items $MAX_ITEMS --output json --region $REGION)
    else
        DATA=$(aws dynamodb scan --table-name "$TABLE_FROM" --max-items $MAX_ITEMS --output json --region $REGION --starting-token $NEXT_TOKEN)
    fi
    # prepare and write to target table
    echo "Preparing data for $TABLE_TO in $TEMP_PAYLOAD_FILE..."
    echo "$DATA" | jq "{ \"$TABLE_TO\": [ .Items[] | { PutRequest: { Item: . } } ] }" > $TEMP_PAYLOAD_FILE
    echo "Writing data to $TABLE_TO..."
    aws dynamodb batch-write-item --region $REGION --request-items file://"$TEMP_PAYLOAD_FILE" >> $TEMP_WRITE_REPORT_FILE
    # capture NEXT_TOKEN
    NEXT_TOKEN=$(echo "$DATA" | jq -r '.NextToken')
    echo "Next token: $NEXT_TOKEN"
    echo
}

# dynamodb batch write has a 25 item limit
MAX_ITEMS=25

# read and write loop
read_and_write
while [[ "$NEXT_TOKEN" != "null" ]]; do
    read_and_write
done

# clean up
rm $TEMP_PAYLOAD_FILE