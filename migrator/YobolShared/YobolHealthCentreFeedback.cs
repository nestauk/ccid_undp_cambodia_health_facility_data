public class YobolHealthCentreFeedback
{
    public string? __typename { get; set; }

    // disambiguation properties
    public int? _version { get; set; }
    public long? _lastChangedAt { get; set; }
    public bool? _deleted { get; set; }

    // data properties
    public Guid id { get; set; }
    public int? version { get; set; }
    public DateTime? updatedAt { get; set; }
    public DateTime? createdAt { get; set; }

    // location
    public string? province_km { get; set; }
    public string? province_en { get; set; }
    public string? district_km { get; set; }
    public string? district_en { get; set; }
    public string? health_centre_km { get; set; }
    public string? health_centre_en { get; set; }

    // date
    public string? visited_date { get; set; }
    public DateTime? submitted { get; set; }

    // feedback
    public string? bribe { get; set; }
    public int? speed { get; set; }
    public int? friendliness { get; set; }
    public int? quality { get; set; }

    public static YobolHealthCentreFeedback From(SortableSemanticFeedback old)
    {
        var hcf = new YobolHealthCentreFeedback();
        hcf.id = old.id;
        hcf.__typename = "HealthCentreFeedback";
        hcf.version = 4;

        // data fields
        hcf.createdAt = DateTime.Parse(old.createdAt!);
        hcf.updatedAt = DateTime.Now;

        // disambiguation fields

        // _lastChangedAt should be seconds since epoch
        hcf._lastChangedAt = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
        hcf._deleted = false;
        hcf._version = 1;

        // location
        hcf.health_centre_km = old.health_centre_km;
        hcf.health_centre_en = old.health_centre_en;
        hcf.province_km = old.province_km;
        hcf.province_en = old.province_en;

        // dates
        hcf.visited_date = old.date;
        hcf.submitted = DateTime.Parse(old.submitted!);

        // feedback
        hcf.bribe = old.bribe;
        hcf.speed = old.speed;
        hcf.friendliness = old.friendliness;
        hcf.quality = old.quality;

        return hcf;
    }
}