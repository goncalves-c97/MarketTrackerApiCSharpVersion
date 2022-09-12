using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketTracker.Models
{
    [Table("REL_MERCADO_PRODUTO_PRECO")]
    public class REL_MERCADO_PRODUTO_PRECO
    {
        [Key]
        public int ID { get; set; }
        public int ID_MERCADO { get; set; }
        public int ID_PRODUTO { get; set; }
        public DateTime DATA_HORA_REGISTRO { get; set; }
        public decimal PRECO { get; set; }

        [NotMapped]
        public MERCADOS MERCADO { get; set; }
        [NotMapped]
        public PRODUTOS PRODUTO { get; set; }

    }
}
