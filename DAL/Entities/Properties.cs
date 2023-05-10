using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Properties
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }

        public DateTime Observed { get; set; }

        public string ParameterId { get; set; }

        public string StationId { get; set; }

        public double Value { get; set; }
    }
}
