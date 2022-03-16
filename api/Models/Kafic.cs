using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Kafici")]
    public class Kafic
    {
        [Key]
        public int id { get; set; }
        public string ime { get; set; }
        public List<Sto> stolovi { get; set; }

    }
}
