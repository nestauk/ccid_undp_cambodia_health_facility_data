public class YobolHealthFeedback
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

    public static YobolHealthFeedback From(YobolHealthCentreFeedback old)
    {
        var hff = new YobolHealthFeedback();
        hff.id = old.id;
        hff.__typename = "YobolHealthFeedback";
        hff.version = 5;

        // data fields
        hff.createdAt = old.createdAt;
        hff.updatedAt = DateTime.Now;

        // disambiguation fields

        // _lastChangedAt should be seconds since epoch
        hff._lastChangedAt = old._lastChangedAt;
        hff._deleted = old._deleted;
        hff._version = old._version;

        // location
        hff.health_centre_km = old.health_centre_km;
        hff.health_centre_en = old.health_centre_en;
        hff.district_en = old.district_en;
        hff.district_km = old.district_km;
        hff.province_km = old.province_km;
        hff.province_en = old.province_en;

        // dates
        hff.visited_date = old.visited_date;
        hff.submitted = old.submitted;

        // feedback
        hff.bribe = old.bribe;
        hff.speed = old.speed;
        hff.friendliness = old.friendliness;
        hff.quality = old.quality;

        return hff;
    }
}