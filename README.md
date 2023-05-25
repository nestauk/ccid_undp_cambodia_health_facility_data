# Cambodian health facility data

This repository serves as a resource for preparing translations of the provinces, villages, and health centres in Cambodia.

## Licensing

- Source data from [Open Development Cambodia](https://opendevelopmentcambodia.net/) is licensed under the [Creative Commons Attribution 3.0 Unported](https://creativecommons.org/licenses/by/3.0/) license.
- Derived data, created by Nesta and partner agencies is licensed under the [Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International](https://creativecommons.org/licenses/by-nc-sa/4.0/) license.
- Scripts in this repository created by Nesta are licensed under the [MIT license](LICENSE).

## How to use

- Translation sheets are in CSV format in the `translation/` directory.
- These can be edited in any spreadsheet tool (including Google Sheets, or Excel).
- When finished, save the sheet as CSV in the `translation/` directory.
- Add, commit and push your changes to this repository.
- This CSV data can be imported into the UNDP Cambodia Yobol application.

### Manual translation

Key fields in each CSV for translation:

| Property                   | Description              |
| -------------------------- | ------------------------ |
| `properties/PNAME`         | Province Name in English |
| `properties/PNAME_km`      | Province Name in Khmer   |
| `properties/VNAME`         | Village name in English  |
| `properties/VNAME_km`      | Village name in Khmer    |
| `properties/FACILITNAM`    | Facility Name in English |
| `properties/FACILITNAM_km` | Facility Name in Khmer   |

## About the data

The source data resource is:

- [Health facilities in Cambodia (GeoJSON)](https://data.opendevelopmentcambodia.net/en/dataset/health-facility-of-cambodia-2010/resource/0aeea7e7-b2cc-4214-985b-65617ce8cea0)

This is sourced from the [Health Facilities in Cambodia (2010)](https://data.opendevelopmentcambodia.net/en/dataset/health-facility-of-cambodia-2010) dataset provided by [Open Development Cambodia](https://opendevelopmentcambodia.net/), and contains:

- Health posts
- Health centres
- Referral hospitals

This data is licensed under the [Create Commons Attribution](https://creativecommons.org/licenses/by/3.0/) license.

### GeoJSON feature properties

The GeoJSON features in this data resource have the following properties:

| Property     | Description               |
| ------------ | ------------------------- |
| `PCODE`      | Province code             |
| `PNAME`      | Province Name             |
| `DNAME`      | District Name             |
| `CNAME`      | Commune Name              |
| `DCODE`      | District Code             |
| `CCODE`      | Commune Code              |
| `VNAME`      | Village name              |
| `ODCODE`     | Operational District Code |
| `ODName`     | Operational District Name |
| `FACILITCOD` | Facility Code             |
| `FACILITNAM` | Facility Name             |
| `COVERNAME`  | Name of Coverage Area     |

Names are provided in **English**.

## Starting from scratch

It's possible to recreate the initial templates from scratch by following this process.

### Prerquisites

- [csvkit](https://csvkit.readthedocs.io/en/latest/)

To install the prerequisites on Mac OS:

```bash
./init.sh
```

### Create blank templates

First, delete any existing templates and translation data, then use the creation script to prepare templates:

```bash
rm template/*
rm translation/*
./create-translation-templates.sh
```

1. The GeoJSON records for all features are exported to CSV format.
2. Blank CSV translation templates are created in the `template/` directory.
3. The translation templates are copied to the `translation/` directory.
4. `original-translation-data/khmer-health-select.json` is merged with the tables in the `translation/` directory.
