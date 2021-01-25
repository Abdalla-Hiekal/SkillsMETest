using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsTest.Models.ViewModels
{
    public class QuickAssignment
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public int? PastureId { get; set; }
    }
}
