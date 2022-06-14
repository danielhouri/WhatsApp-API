using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API
{
    public class Message
    {
        [Key]
        public int? Id { get; set; }
        public string? Content { get; set; }

        public bool? Send { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Created { get; set; }

        [JsonIgnore]
        public virtual Contact? Contact { get; set; }
    }
}