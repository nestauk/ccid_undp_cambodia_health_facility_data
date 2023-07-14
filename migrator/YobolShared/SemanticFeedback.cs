public class SemanticFeedback
{
    public string? __typename { get; set; }
    public Guid id { get; set; }
    public int? version { get; set; }
    public string? updatedAt { get; set; }
    public string? createdAt { get; set; }
    public string? submitted { get; set; }

    public Feedback feedback { get; set; } = new Feedback();

    public class Feedback
    {
        public Questions questions { get; set; } = new Questions();
        public Verify verify { get; set; } = new Verify();
        public Search search { get; set; } = new Search();

        public class Questions
        {
            public string? bribe { get; set; }
            public int? speed { get; set; }
            public int? friendliness { get; set; }
            public int? quality { get; set; }
        }

        public class Verify
        {
            public string? phone { get; set; }
            public string? code { get; set; }
        }

        public class Search
        {
            public string? date { get; set; }
            public Facility facility { get; set; } = new Facility();

            public class Facility
            {
                public string? district_km { get; set; }
                public string? district_en { get; set; }
                public string? health_centre_km { get; set; }
                public string? health_centre_en { get; set; }
                public string? province_km { get; set; }
                public string? province_en { get; set; }
            }
        }
    }
}


