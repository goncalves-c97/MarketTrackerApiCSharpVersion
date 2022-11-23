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
        public decimal LAT { get; set; }
        public decimal LONG { get; set; }
        public string ESTADO { get; set; }
        public string CIDADE { get; set; }
        public string CEP { get; set; }
        public string ENDERECO { get; set; }
        public string NUMERO { get; set; }

        public string ENDERECO_COMPLETO { get => $"{ENDERECO} - {NUMERO} / {CIDADE}-{ESTADO} ({CEP})"; }
    }
}
