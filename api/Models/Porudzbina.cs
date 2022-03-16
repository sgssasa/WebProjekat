using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class Porudzbina
    {
        public int id { get; set; }
        public bool obradjena { get; set; }
         
        public List<Porudzbenica> proizvodi { get; set; }
        [JsonIgnore]
        public Stolica stolica { get; set; }
    }
}
