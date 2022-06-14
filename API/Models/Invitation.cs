using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API
{
    public class Invitation
    {
        [Key]
        [Required]
        public string? From { get; set; }

        [Required]
        public string? To { get; set; }

        [Required]
        public string? Server { get; set; }
    }
}