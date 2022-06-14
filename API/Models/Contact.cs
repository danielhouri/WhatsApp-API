using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API
{
    public class Contact
    {
        [Key, Column(Order = 0)]
        public string? Id { get; set; }

        [Key, Column(Order = 1)]
        [JsonIgnore]
        public virtual User? User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Message>? Message { get; set; }

        public string? Name { get; set; }

        public string? Server { get; set; }

        public string? Last { get; set; }

        public DateTime? LastDate { get; set; }

    }
}