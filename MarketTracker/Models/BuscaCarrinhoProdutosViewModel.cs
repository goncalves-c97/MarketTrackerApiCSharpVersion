using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTracker.Models
{
    public class BuscaCarrinhoProdutosViewModel
    {
        public string MERCADO { get; set; }
        public string ENDERECO_MERCADO { get; set; }
        public double DISTANCIA { get; set; }
        public decimal VALOR_TOTAL { get; set; }
        public List<int> PRODUTOS_DISPONIVEIS { get; set; } = new List<int>();
    }
}
