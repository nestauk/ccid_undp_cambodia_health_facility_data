# cambodia-health-facility-data

This repository contains scripts to manage information about health centre data in Cambodia for the UNDP Cambodia feedback project.

## migrator

The data structures for the Yobol application have iterated several times. The [migrator/](migrator/) directory contains console applications for maanging data and the data structures stored in our DynamoDB table.

- The current format is: `SortableSemanticFeedback`
- All data has been migrated to this format.

### scripts

| script       | purpose                                                                                         |
| ------------ | ----------------------------------------------------------------------------------------------- |
| `migrate.sh` | Launches the YobolMigrator console application with parameters to migrate data between formats. |
| `repair.sh`  | Repairs errors in the recorded data.                                                            |

### repairing data

The current repair modes available are:

| mode                          | purpose                                                                                                                                                                                                    |
| ----------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `ReIndexFriendlinessAndSpeed` | The `friendliness` and `speed` values were indexed `0-4` but should have been indexed `1-5`. This repair increments all values for these aspects of feedback that were submitted before a given timestamp. |

## deprecated

The deprecated directory contains code to support previous work.

The [deprecated/2010 data](deprecated/2010%20data/) directory contains scripts to adapt and use 2010 health facility data from [Open Development Cambodia](https://opendevelopmentcambodia.net/).
