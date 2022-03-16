using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Sto
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(10)]
        public string oznaka { get; set; }
        public List<Stolica> stolice { get; set; }
        [JsonIgnore]
        public Kafic kafic { get; set; }

    }
}
