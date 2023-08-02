using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

public class ReIndexFriendlinessAndSpeed
{
    public static async Task RepairAsync(string tableName, DateTime before, bool write)
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
            var ssfs = items
                .Select(context.FromDocument<SortableSemanticFeedback>)
                .Where(feedback => DateTime.Parse(feedback.submitted!) <= before);
            Console.WriteLine($"{ssfs.Count()} SortableSemanticFeedback items to repair.");

            Console.WriteLine("Repairing items...");
            foreach (var feedback in ssfs)
            {
                feedback.friendliness += 1;
                feedback.speed += 1;
                feedback.feedback.questions.friendliness += 1;
                feedback.feedback.questions.speed += 1;
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
            Console.WriteLine($"{ssfs.Count()} items repaired.");
            Console.WriteLine();

        }

    }



}