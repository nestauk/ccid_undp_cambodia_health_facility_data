using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

public class MigrateToYobolHealthFacilityFeedback
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
            var yobol_hcfs = documents.Select(d => context.FromDocument<YobolHealthCentreFeedback>(d));
            Console.WriteLine($"{yobol_hcfs.Count()} YobolHealthCentreFeedback objects inflated.");
            Console.WriteLine();

            Console.WriteLine("Preparing items...");
            var yobol_hffs = yobol_hcfs.Select(hcf => YobolHealthFeedback.From(hcf));
            Console.WriteLine($"{yobol_hffs.Count()} YobolHealthFeedback objects ready to write.");
            Console.WriteLine();

            Console.WriteLine("Writing items...");
            var target_table = Table.LoadTable(client, target_table_name);
            foreach (var feedback in yobol_hffs)
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
            Console.WriteLine($"{yobol_hffs.Count()} items written.");
            Console.WriteLine();
        }
    }

}