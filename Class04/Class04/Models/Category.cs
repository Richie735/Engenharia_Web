using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Class04.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} length must be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(256, ErrorMessage = "{0} length can not exceed {1} characters")]
        public string Description { get; set; }

        public bool State { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
