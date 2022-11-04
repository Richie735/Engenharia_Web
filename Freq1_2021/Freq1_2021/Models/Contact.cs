using System.ComponentModel.DataAnnotations;

namespace Freq1_2021.Models
{
    public class Contact
    {
        [Key]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} length must be between {2} and {1}")]
        [RegularExpression(@"[a-zA-Z\d]{3,}", ErrorMessage = "Not Valid")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Name { get; set; }

        [Required]
        public Boolean Amigo { get; set; } = false;
    }
}
