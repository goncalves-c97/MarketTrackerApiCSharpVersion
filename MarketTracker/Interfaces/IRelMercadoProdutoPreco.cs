using MarketTracker.Models;
using System.Collections.Generic;

namespace MarketTracker.Interfaces
{
    public interface  IRelMercadoProdutoPreco
    {
        public bool AdicionarNovaRelacaoMercadoProdutoPreco(int mercadoID, int produtoID, decimal preco);
        public List<REL_MERCADO_PRODUTO_PRECO> BuscarPorTexto(string textoDeBusca);
    }
}
