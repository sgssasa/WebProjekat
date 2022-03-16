using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Porudzbenica
    {
        public int id { get; set; }
        [JsonIgnore]
        public Proizvod proizvod{get; set;}
        [JsonIgnore]
        public Porudzbina porudzbina{get; set;}
        public int kolicina { get; set; }
    }
}
