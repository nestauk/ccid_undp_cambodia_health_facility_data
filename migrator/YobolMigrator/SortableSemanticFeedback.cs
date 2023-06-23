public class SortableSemanticFeedback
{
    public string? __typename { get; set; }
    public Guid id { get; set; }
    public int? version { get; set; }
    public string? updatedAt { get; set; }
    public string? createdAt { get; set; }
    public string? submitted { get; set; }

    public string? health_centre_km { get; set; }
    public string? health_centre_en { get; set; }
    public string? province_km { get; set; }
    public string? province_en { get; set; }

    public string? date { get; set; }
    public string? bribe { get; set; }
    public int? speed { get; set; }
    public int? friendliness { get; set; }
    public int? quality { get; set; }

    public SemanticFeedback.Feedback feedback { get; set; } = new SemanticFeedback.Feedback();

    public static SortableSemanticFeedback From(SemanticFeedback old)
    {
        var ssf = new SortableSemanticFeedback();
        ssf.id = old.id;
        ssf.__typename = "SortableSemanticFeedback";
        ssf.version = 3;
        ssf.submitted = old.submitted;
        ssf.createdAt = old.createdAt;
        ssf.updatedAt = DateTime.Now.ToUniversalTime().ToString("o");
        ssf.health_centre_km = old.feedback.search.facility.health_centre_km;
        ssf.health_centre_en = old.feedback.search.facility.health_centre_en;
        ssf.province_km = old.feedback.search.facility.province_km;
        ssf.province_en = old.feedback.search.facility.province_en;
        ssf.date = old.feedback.search.date;
        ssf.bribe = old.feedback.questions.bribe;
        ssf.speed = old.feedback.questions.speed;
        ssf.friendliness = old.feedback.questions.friendliness;
        ssf.quality = old.feedback.questions.quality;
        ssf.feedback = old.feedback;
        return ssf;
    }
}