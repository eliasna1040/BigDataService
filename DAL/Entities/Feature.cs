using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }
        public string Id { get; set; }
        public Geometry Geometry { get; set; }


        public string Type { get; set; }

        public Properties Properties { get; set; }
    }
}
