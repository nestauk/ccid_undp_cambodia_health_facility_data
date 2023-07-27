using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

public class MigrateToYobolHealthCentreFeedback
{
    public static async Task RepairAsync(string source_table_name, string target_table_name, bool write)
    {
        using (var client = new AmazonDynamoDBClient(Amazon.RegionEndpoint.APSoutheast1))
        using (var context = new DynamoDBContext(client))
        {
            var source_table = Table.LoadTable(client, source_table_name);
            var config = new ScanOperationConfig()
            {
                Limit = 100,
                Select = SelectValues.AllAttributes
            };

            Console.WriteLine("Scanning table...");
            var search = source_table.Scan(config);
            var documents = new List<Document>();
            while (!search.IsDone)
            {
                documents.AddRange(await search.GetNextSetAsync());
                Console.Write(".");
            }
            Console.WriteLine();
            Console.WriteLine($"{documents.Count} documents in source table.");
            Console.WriteLine();

            Console.WriteLine("Inflating SortableSemanticFeedback items...");
            var sortable_semantic_feebacks = documents.Select(d => context.FromDocument<SortableSemanticFeedback>(d));
            Console.WriteLine($"{sortable_semantic_feebacks.Count()} SortableSemanticFeedback objects inflated.");
            Console.WriteLine();

            Console.WriteLine("Preparing items...");
            var health_centre_feedbacks = sortable_semantic_feebacks.Select(ssf => YobolHealthCentreFeedback.From(ssf));
            Console.WriteLine($"{health_centre_feedbacks.Count()} YobolHealthCentreFeedback objects ready to write.");
            Console.WriteLine();

            Console.WriteLine("Writing items...");
            var target_table = Table.LoadTable(client, target_table_name);
            foreach (var feedback in health_centre_feedbacks)
            {
                if (write)
                {
                    await target_table.UpdateItemAsync(context.ToDocument(feedback));
                    Console.Write("W");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"{health_centre_feedbacks.Count()} items written.");
            Console.WriteLine();
        }
    }

}