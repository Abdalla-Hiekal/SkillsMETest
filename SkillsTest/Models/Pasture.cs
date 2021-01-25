using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsTest.Models
{
    public class Pasture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Temperature { get; set; }
        public string GrassCondition { get; set; }

        //Relation with Cattle
        public virtual ICollection<Cattle> Cattles { get; set; }
    }
}
