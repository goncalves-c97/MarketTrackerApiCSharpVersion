using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTracker.Models
{
    public class BuscaProdutoViewModel
    {
        public string PRODUTO { get; set; }
        public string MERCADO { get; set; }
        public string ENDERECO_MERCADO { get; set; }
        public decimal PRECO { get; set; }
        public double DISTANCIA { get; set; }
        public DateTime DATA_HORA_REGISTRO { get; set; }
    }
}
