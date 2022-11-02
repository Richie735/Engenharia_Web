using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Class02.Models
{
    public class Person
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Range(18, 100, ErrorMessage = "{0} must have values between {1} and {2}")]
        public int Age { get; set; }
    }
}
