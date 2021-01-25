using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsTest.Models
{
    public class Cattle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Age { get; set; }
        public int Price { get; set; }
        public double Weight { get; set; }
        public string Type { get; set; }
        public string HealthStatus { get; set; }
        public string Color { get; set; }

        //Relation with Pasture
        public int? PastureId { get; set; }
        public virtual Pasture Pasture { get; set; }
    }
}
