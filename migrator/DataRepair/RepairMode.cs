using System;
namespace DataRepair
{
	public enum RepairMode
	{
		None,
		SetConflictResolutionValues, // add missing values for DataStore conflict resolution
		SetMissingDistrictValues, // set district values that were not been stored during beta
	}
}

