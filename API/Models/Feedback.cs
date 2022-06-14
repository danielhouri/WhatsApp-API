using System.ComponentModel.DataAnnotations;

namespace Rating.Models
{
    public class Feedback
    {
        [Required]
        [Key]
        public string username { get; set; }
        [Required]
        [Range(0,5)]
        public int rate { get; set; }
        [Required]
        [StringLength(100)]
        public string description { get; set; }
        [Required]
        public DateTime time { get; set; }
    }
}