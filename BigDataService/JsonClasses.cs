using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDataService
{
    public class Feature
    {
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Properties
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

    public class DataSet
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }
    }
}
