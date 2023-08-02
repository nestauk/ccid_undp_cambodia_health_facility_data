using System;
using System.Globalization;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using CsvHelper;

public class SetMissingDistrictValues
{
    private static IEnumerable<HealthCentre> GetHealthCentres(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<HealthCentre>().ToList();
    }

    public static async Task RepairAsync(string tableName, string pathToHealthCentreData, bool write)
    {
        Console.WriteLine("Reading health centre data...");
        var healthCentres = GetHealthCentres(pathToHealthCentreData);
        Console.WriteLine($"{healthCentres.Count()} health centres read.");
        Console.WriteLine();

        using var client = new AmazonDynamoDBClient(Amazon.RegionEndpoint.APSoutheast1);
        using var context = new DynamoDBContext(client);
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
        var feedbackToFix = items
            .Select(context.FromDocument<YobolHealthCentreFeedback>);

        Console.WriteLine($"{feedbackToFix.Count()} YobolHealthCentreFeedback items to repair.");

        Console.WriteLine("Repairing items...");
        var blanks = 0; var fixes = 0;
        foreach (var feedback in feedbackToFix)
        {
            // TODO: apply missing values
            if (string.IsNullOrWhiteSpace(feedback.district_en) || string.IsNullOrWhiteSpace(feedback.district_km))
            {
                blanks++;
                var hc = healthCentres.FirstOrDefault(hc =>
                    (hc.health_centre_en == feedback.health_centre_en || hc.health_centre_km == feedback.health_centre_km) &&
                    (hc.province_en == feedback.province_en || hc.province_km == feedback.province_km));

                if (hc != null)
                {
                    feedback.district_en = hc.district_en;
                    feedback.district_km = hc.district_km;
                    fixes++;
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
                else
                {
                    Console.Write("_");
                }
            }
        }
        Console.WriteLine();
        Console.WriteLine($"{blanks} repairs attempted.");
        Console.WriteLine($"{fixes} repairs made.");
        Console.WriteLine();
    }


}