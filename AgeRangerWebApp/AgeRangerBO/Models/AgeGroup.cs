using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerBO.Models
{
    public class AgeGroup
    {
        public long? Id { get; set; }

        public long? MinAge { get; set; }

        public long? MaxAge { get; set; }

        [Display(Name = "Age Group")]
        public string Description { get; set; }
    }
}
