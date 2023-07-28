# migrator

This is a data repair tool for the UNDP Cambodia project. It's a small solution to iterate over feedback data and correct errors or issues as needed.

## Usage

```bash
./repair.sh <environment> <repair-mode> <write-enable> <parameter>
```

- `environment` - This informs the feedback data table name. Values are:
  - `dev`
  - `prod`
- `repair-mode` - Which operation to execute over the data. Values are:
  - `ReIndexFriendlinessAndSpeed` - fixes 0-indexed values found for `friendless` and `speed`
  - `AddConflictResolutionValues` - adds values for `_version`, `_lastChangedAt`, `_deleted`
  - `MigrateToYobolHealthCentreFeedback` - converts to simpler, flatter, `YobolHealthCentreFeedback` data type
  - `SetMissingDistrictValues` - applies district data to feedback that does not have district information
- `write-enable` - Determines whether to write back to the database or not. Values are:
  - `true` - write enabled, write changes to the database
  - `false` - this is a dry run, enact the changes but do not write back
- `parameter` - Optional additional parameter required by some repair operations

## Repairs

### `ReIndexFriendlinessAndSpeed`

During the beta test, a bug was introduced that caused `speed` and `friendliness` to be 0-indexed values. (They should really be in the range 1-5.)

This operation corrects all values after a given date by incrementing their value. Provide the date as the `parameter` argument to the repair script in ISO 8601 format (eg. `yyyy-mm-dd`).

### `AddConflictResolutionValues`

DataStore requires values for `_version`, `_lastChangedAt`, `_deleted` - which were not originally included. (NB. We've enabled conflict resolution values now, but not actually using DataStore - as it's not really the right technology for us.) With conflict resolution enabled, all data values need those additional fields.

This operation adds sensible default values for the conflict resolution fields to all existing entries.

### `MigrateToYobolHealthCentreFeedback`

This operation converts existing `SortableSemanticFeedback` data items into `YobolHealthCentreFeedback` items, and stores them in the table defined in `parameter`.

### `SetMissingDistrictValues`

This operation adds district information to `YobolHealthCentreFeedback` data items that do not have it set. Provide the path to `health-centres-2022.csv` in `parameter`.

## Projects

| Project       | Purpose                                                                                             |
| ------------- | --------------------------------------------------------------------------------------------------- |
| `DataRepair`  | Command line tool to enact repairs against DynamoDb tables. Invoked through the `repair.sh` script. |
| `YobolShared` | Shared data structures for this tool.                                                               |
