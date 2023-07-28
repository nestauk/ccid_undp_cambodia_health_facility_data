# cambodia-health-facility-data

This repository contains scripts to manage information about health centre data in Cambodia for the UNDP Cambodia feedback project.

## `health-centre-data/`

This directory contains a copy of our most current health centre data for the currently supported provinces.

See [health-centre-data/README.md](health-centre-data/README.md) for more information.

## `migrator/`

The data structures for the Yobol application have iterated several times. The [migrator/](migrator/) directory contains a console application for managing and repairing the data structures stored in our DynamoDB table.

See [migrator/README.md](migrator/README.md) for more information.

## `opensearch-backfill/`

The `opensearch-backfill/` directory contains a script to retrospectively push existing data into opensearch - essential for the `@searchable` graphql models that the application relies on.

It has been run for existing data, and should not be needed again.

## `deprecated/`

The deprecated directory contains code to support previous work.

The [deprecated/2010 data](deprecated/2010%20data/) directory contains scripts to adapt and use 2010 health facility data from [Open Development Cambodia](https://opendevelopmentcambodia.net/).
