using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Class03.Models
{
    public class FileViewModel
    {
        [Required]
        [RegularExpression(@"^.+\.([pP][dD][fF])$", ErrorMessage = "Only Pdf Files")]
        public string Name { get; set; }

        [Display(Name ="Size in Bytes")]
        public long Size { get; set; }
    }
}
