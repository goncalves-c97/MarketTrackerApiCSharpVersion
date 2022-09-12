using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketTracker.Models
{
    [Table("PRODUTOS")]
    public class PRODUTOS
    {
        [Key]
        public int ID { get; set; }
        public string NOME { get; set; }
        public string COD_BARRAS { get; set; }
    }
}
