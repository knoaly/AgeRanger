using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRangerBO.Models
{
    public class Person
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "First Name is mandatory.")]
        [Display(Description = "First Name")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "First Name must be between {2} and {1} characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is mandatory.")]
        [Display(Description = "Last Name")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Last Name must be between {2} and {1} characters.")]
        public string LastName { get; set; }

        [Range(1, 99999, ErrorMessage = "Age must be between {1} and {2}.")]
        public long? Age { get; set; }

        public AgeGroup Agegroup { get; set; }
    }
}
