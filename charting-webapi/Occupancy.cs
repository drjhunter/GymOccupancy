using System;
using System.Text.Json.Serialization;

namespace charting_webapi
{
    public class Occupancy
    {
        [JsonPropertyName("gymid")]
        public string GymId { get; set; }
        public int NumberOfPeople { get; set; }

        public DateTime Date { get; set; }

    }
}
