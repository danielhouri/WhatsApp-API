using System.ComponentModel.DataAnnotations;

namespace API
{
    public class Transfer
    {
        [Key]
        [Required]
        public string? From { get; set; }

        [Required]
        public string? To { get; set; }

        [Required]
        public string? Content { get; set; }

    }
}