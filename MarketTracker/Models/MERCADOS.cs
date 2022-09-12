using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketTracker.Models
{
    [Table("MERCADOS")]
    public class MERCADOS
    {
        [Key]
        public int ID { get; set; }
        public string NOME { get; set; }
    }
}
