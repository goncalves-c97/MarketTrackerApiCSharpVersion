using MarketTracker.Models;
using System.Collections.Generic;

namespace MarketTracker.Interfaces
{
    public interface  IRelMercadoProdutoPreco
    {
        public REL_MERCADO_PRODUTO_PRECO AdicionarNovaRelacaoMercadoProdutoPreco(int mercadoID, int produtoID, decimal preco);
        public List<REL_MERCADO_PRODUTO_PRECO> BuscarPorTexto(string textoDeBusca);
        public List<REL_MERCADO_PRODUTO_PRECO> BuscarPorIdsProdutos(List<int> idsProdutos);
        public List<REL_MERCADO_PRODUTO_PRECO> ListarTodos();
    }
}
