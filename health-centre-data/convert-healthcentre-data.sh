#!/bin/bash

# uses csvkit for csvjson - to convert csv to json
# run from the root of the project, eg. scripts/convert-healthcentre-data.sh

SOURCE_FILE=health-centres-2022.csv
DEST_FILE=health-centres-2022.json
echo "Converting ${SOURCE_FILE} to ${DEST_FILE}"

NONINTERACTIVE=1 brew install csvkit --quiet
csvjson -i 2 $SOURCE_FILE > $DEST_FILE
