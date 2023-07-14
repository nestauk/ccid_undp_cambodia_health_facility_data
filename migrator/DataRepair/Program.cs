using System.Diagnostics.Tracing;
using DataRepair;

var mode = Enum.Parse<RepairMode>(args[0]);
var table = args[1];
var write = bool.Parse(args[2]);

Console.WriteLine("Preparing to repair...");
Console.WriteLine("Mode:  " + mode);
Console.WriteLine("Table: " + table);
Console.WriteLine("Write: " + (write ? "enabled" : "disabled"));
Console.WriteLine();

switch (mode)
{
    case RepairMode.None:
        Console.WriteLine("Please specify a repair.");
        Console.WriteLine($"Options: {string.Join(", ", Enum.GetValues<RepairMode>())}");
        return;

    case RepairMode.ReIndexFriendlinessAndSpeed:
        Console.WriteLine("Repairing values for friendliness and speed...");
        var date = DateTime.Parse(args[3]);
        Console.WriteLine("Before: " + date.ToString());
        Console.WriteLine();
        await ReIndexFriendlinessAndSpeed.RepairAsync(table, date, write);
        break;
}
