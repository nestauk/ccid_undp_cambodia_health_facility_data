using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;

var tableName_from = args[0];
var tableName_to = args[1];
var write = bool.Parse(args[2]);

Console.WriteLine("Preparing to migrate...");
Console.WriteLine("From:  " + tableName_from);
Console.WriteLine("To:    " + tableName_to);
Console.WriteLine("Write: " + (write ? "enabled" : "disabled"));

using (var client = new AmazonDynamoDBClient(Amazon.RegionEndpoint.APSoutheast1))
using (var context = new DynamoDBContext(client))
{
    var table_from = Table.LoadTable(client, tableName_from);
    var table_to = Table.LoadTable(client, tableName_to);

    var config = new ScanOperationConfig()
    {
        Limit = 1000,
        Select = SelectValues.AllAttributes
    };

    var search = table_from.Scan(config);
    var items = await search.GetNextSetAsync();
    Console.WriteLine($"Found {items.Count} items to migrate.");

    var writeBatch = table_to.CreateBatchWrite();

    Console.WriteLine("Checking items...");
    var ssfs = items
            .Select(item => context.FromDocument<SemanticFeedback>(item))
            .Select(sf => SortableSemanticFeedback.From(sf));
    Console.WriteLine($"Succesfully read {ssfs.Count()} SemanticFeedback items.");

    if (write)
    {
        Console.WriteLine("Migrating items...");
        foreach (var sortable in ssfs)
        {
            writeBatch.AddDocumentToPut(context.ToDocument(sortable));
        }
        await writeBatch.ExecuteAsync();
    }
    Console.WriteLine("Done.");
}
