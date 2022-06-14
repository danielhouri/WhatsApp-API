using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "This filed is required!")]
        public string? Username { get; set; }

        public string? Name { get; set; }

        [Required(ErrorMessage = " This filed must contain letters and chars!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Image { get; set; }

        [JsonIgnore]
        public ICollection<Contact>? Contacts { get; set; }
    }
}