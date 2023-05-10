using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDataService.Classes
{
    public class JsonFeature
    {
        [JsonProperty("geometry")]
        public JsonGeometry Geometry { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public JsonProperties Properties { get; set; }
    }

    public class JsonGeometry
    {
        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class JsonProperties
    {
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("observed")]
        public DateTime Observed { get; set; }

        [JsonProperty("parameterId")]
        public string ParameterId { get; set; }

        [JsonProperty("stationId")]
        public string StationId { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }

    public class JsonDataSet
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public List<JsonFeature> Features { get; set; }
    }
}
