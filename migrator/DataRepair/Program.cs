using System.Diagnostics.Tracing;
using DataRepair;

var mode = Enum.Parse<RepairMode>(args[0]);
var table = args[1];
var write = bool.Parse(args[2]);
var param = args.Length >= 4 ? args[3] : null;

Console.WriteLine("Preparing to repair...");
Console.WriteLine("Mode:  " + mode);
Console.WriteLine("Table: " + table);
Console.WriteLine("Write: " + (write ? "enabled" : "disabled"));
Console.WriteLine("Param: " + param ?? "(null)");
Console.WriteLine();

switch (mode)
{
    case RepairMode.None:
        Console.WriteLine("Please specify a repair.");
        Console.WriteLine($"Options: {string.Join(", ", Enum.GetValues<RepairMode>())}");
        return;

    case RepairMode.ReIndexFriendlinessAndSpeed:
        Console.WriteLine("Repairing values for friendliness and speed...");
        var date = DateTime.Parse(param!);
        Console.WriteLine("Before: " + date.ToString());
        Console.WriteLine();
        await ReIndexFriendlinessAndSpeed.RepairAsync(table, date, write);
        break;

    case RepairMode.AddConflictResolutionValues:
        Console.WriteLine("Adding missing conflict resolution values...");
        Console.WriteLine();
        await AddConflictResolutionValues.RepairAsync(table, write);
        break;

    case RepairMode.MigrateToYobolHealthCentreFeedback:
        Console.WriteLine("Migrating to HealthCentreFeedback...");
        Console.WriteLine();
        var target_table = args[3]!;
        await MigrateToYobolHealthCentreFeedback.RepairAsync(table, target_table, write);
        break;

    case RepairMode.SetMissingDistrictValues:
        Console.WriteLine("Setting missing district values...");
        Console.WriteLine();
        var health_centre_csv_path = args[3]!;
        await SetMissingDistrictValues.RepairAsync(table, health_centre_csv_path, write);
        break;

    default:
        Console.WriteLine($"{mode} not supported.");
        break;
}
