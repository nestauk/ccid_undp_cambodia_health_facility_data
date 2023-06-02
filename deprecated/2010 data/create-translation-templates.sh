#!/bin/bash

# create template csv files from the input geojson
for input_path in ./input/*.geojson; do
    input_file=$(basename ${input_path})
    input_file_noext=$(echo "${input_file%%.*}")
    csv_conversion_file="${input_file_noext}_en.csv"
    csv_with_km_file="${input_file_noext}_en_km.csv"
    csv_with_km_ordered="${input_file_noext}_en_km.csv"

    echo "Processing ${input_path}..."

    # convert to CSV
    in2csv --format json -k "features" ${input_path} > template/${csv_conversion_file}

    # add the PNAME_km, VNAME_km, and FACILITNAM_km columns
    sed '1s/$/,\properties\/PNAME_km,properties\/VNAME_km,properties\/FACILITNAM_km/; 2,$s/$/,,,/' template/${csv_conversion_file} > intermediate/${csv_with_km_file}

    # use a subset of columns
    column_order="type,properties/PCODE,properties/PNAME,properties/PNAME_km,properties/DCODE,properties/DNAME,properties/CCODE,properties/CNAME,properties/VCODE,properties/VNAME,properties/VNAME_km,properties/ODCODE,properties/ODNAME,properties/FACILITCOD,properties/FACILITNAM,properties/FACILITNAM_km,geometry/type,geometry/coordinates/0,geometry/coordinates/1"
    csvcut -c ${column_order} intermediate/${csv_with_km_file} > template/${csv_with_km_ordered}
done

# create a csv from the original translation json
in2csv --format json -k "facility" original-translation-data/khmer-health-select.json > intermediate/original-khmer-health-select.csv

# rename the Khmer columns
sed -i .backup '1s/PNAME/PNAME_km/;' intermediate/original-khmer-health-select.csv
sed -i .backup '1s/VNAME/VNAME_km/;' intermediate/original-khmer-health-select.csv
sed -i .backup '1s/FACILITNAM/FACILITNAM_km/;' intermediate/original-khmer-health-select.csv

# take a cut of just the columns that matter
csvcut -d , -c properties/FACILITCOD,properties/PNAME_km,properties/VNAME_km,properties/FACILITNAM_km  intermediate/original-khmer-health-select.csv > intermediate/original-khmer-health-select-cut.csv

# merge each translation file with the original data
for translation_path in ./template/*_en.csv; do
    cp ${translation_path} intermediate/translation-merge.csv
    translation_file=$(basename ${translation_path})
    translation_file_noext=$(echo "${translation_file%%.*}")
    translation_file_en_km="${translation_file_noext}_km.csv"
    csvjoin -d , --left -c properties/FACILITCOD intermediate/translation-merge.csv intermediate/original-khmer-health-select-cut.csv > translation/${translation_file_en_km}
done

# copy all files from template/ to translation/
# don't clobber any existing translation sheets
# cp -n template/*_en_km.csv translation/
