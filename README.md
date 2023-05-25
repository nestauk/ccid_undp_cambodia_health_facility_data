# Cambodian health data

## Data pipeline

### Prerquisites

- [csvkit](https://csvkit.readthedocs.io/en/latest/)

To install the prerequisites on a Mac:

```bash
./init.sh
```

### Health facilities GeoJSON

The initial data resource is:

- [Health facilities in Cambodia (GeoJSON)](https://data.opendevelopmentcambodia.net/en/dataset/health-facility-of-cambodia-2010/resource/0aeea7e7-b2cc-4214-985b-65617ce8cea0)

This is sourced from the [Health Facilities in Cambodia (2010)](https://data.opendevelopmentcambodia.net/en/dataset/health-facility-of-cambodia-2010) data set provided by [Open Development Cambodia](https://opendevelopmentcambodia.net/), and contains:

| National hospitals
| Health posts
| Health centres
| Referral hospitals

#### GeoJSON feature properties

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

The various names are all provided in **English** - but we'll need them in **Khmer.**

### Health facility translation sheet

First, the GeoJSON records for all features are exported to CSV format so that they can be easily edited in a spreadsheet.

To create the blank translation templates:

```text
./create-translation-templates.sh
```

1. Blank CSV templates are created in the `template/` directory.
2. Blank CSV templates are copied to the `translation/` directory.
3. `original-translation-data/khmer-health-select.json` is merged with the tables in the `translation/` directory.

### Manual translation

Key fields for translation:

| Property                   | Description              |
| -------------------------- | ------------------------ |
| `properties/PNAME`         | Province Name in English |
| `properties/PNAME_km`      | Province Name in Khmer   |
| `properties/VNAME`         | Village name in English  |
| `properties/VNAME_km`      | Village name in Khmer    |
| `properties/FACILITNAM`    | Facility Name in English |
| `properties/FACILITNAM_km` | Facility Name in Khmer   |
