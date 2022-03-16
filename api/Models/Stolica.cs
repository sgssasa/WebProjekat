using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Stolica
    {
        public int id { get; set; }
        [Required]
        [MaxLength(10)]
        public string oznaka { get; set; }
        public bool slobodna { get; set; }
        public List<Porudzbina> porudzbine { get; set; }
        [JsonIgnore]
        public Sto sto { get; set; }
    }
}
