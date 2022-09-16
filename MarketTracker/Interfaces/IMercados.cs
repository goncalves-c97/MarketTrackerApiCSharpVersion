using MarketTracker.Models;
using System.Collections.Generic;

namespace MarketTracker.Interfaces
{
    public interface IMercados
    {
        public MERCADOS GetByMercadoByID(int mercadoID);
        public List<MERCADOS> BuscaMercadosPorNome(string nomeMercado);
        public MERCADOS AdicionarMercado(string nome);
    }
}
