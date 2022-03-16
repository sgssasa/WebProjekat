using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api.Models
{
    public class Proizvod
    {
        public int id { get; set; }
        public string slika { get; set; }
        [Required]
        [MaxLength(50)]
        public string naziv { get; set; }
        public double cena { get; set; }
        [JsonIgnore]
        public List<Porudzbenica> narudzbine { get; set; }
    }
}
