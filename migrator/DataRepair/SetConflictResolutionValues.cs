using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

public class SetConflictResolutionValues
{
    public static async Task RepairAsync(string tableName, bool write)
    {
        using (var client = new AmazonDynamoDBClient(Amazon.RegionEndpoint.APSoutheast1))
        using (var context = new DynamoDBContext(client))
        {
            var table = Table.LoadTable(client, tableName);
            var config = new ScanOperationConfig()
            {
                Limit = 1000,
                Select = SelectValues.AllAttributes
            };

            Console.WriteLine("Scanning table...");
            var search = table.Scan(config);
            var items = new List<Document>();
            while (!search.IsDone)
            {
                items.AddRange(await search.GetNextSetAsync());
                Console.Write(".");
            }
            Console.WriteLine();
            Console.WriteLine($"{items.Count} items in table.");
            Console.WriteLine();

            Console.WriteLine("Preparing items...");
            var itemsToFix = items
                .Select(context.FromDocument<YobolHealthCentreFeedback>)
                .ToList();

            Console.WriteLine($"{itemsToFix.Count()} SortableSemanticFeedback items to repair.");

            Console.WriteLine("Repairing items...");
            foreach (var feedback in itemsToFix)
            {
                feedback._version = 1;
                feedback._deleted = false;
                feedback._lastChangedAt = ToAwsTimestamp(DateTime.Now);
                // feedback._lastChangedAt = null;
                if (write)
                {
                    await table.UpdateItemAsync(context.ToDocument(feedback));
                    Console.Write("W");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"{itemsToFix.Count()} items repaired.");
            Console.WriteLine();
        }
    }

    public static long ToAwsTimestamp(DateTime dateTime)
    {
        return (long)(dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
    }
}