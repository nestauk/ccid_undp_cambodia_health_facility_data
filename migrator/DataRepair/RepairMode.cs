﻿using System;
namespace DataRepair
{
	public enum RepairMode
	{
		None,
		ReIndexFriendlinessAndSpeed, // reindex these answers (from 0-4 into 1-5)
		AddConflictResolutionValues, // add missing values for DataStore conflict resolution
		MigrateToYobolHealthCentreFeedback, // migrate to the 4th iteration data structure
		SetMissingDistrictValues, // set district values that were not been stored during beta
		MigrateToYobolHealthFeedback, // migrate to 5th iteration data structure
	}
}

