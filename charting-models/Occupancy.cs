using System;
using System.Text.Json.Serialization;

namespace charting_models
{
    public class Occupancy
    {
        [JsonPropertyName("gymid")]
        public string gymid { get; set; }
        public int numberofpeople { get; set; }

        public DateTime date { get; set; }
        public string id { get; set; }

    }
}
