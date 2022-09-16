using MarketTracker.Models;
using System.Collections.Generic;

namespace MarketTracker.Interfaces
{
    public interface IProdutos
    {
        public PRODUTOS GetByProdutoByID(int mercadoID);
        public List<PRODUTOS> BuscaProdutosPorNome(string nomeMercado);

        /// <summary>
        /// Adiciona o produto e retorna o produto inserido no banco, caso já exista o produto, este é retornado
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="codigoBarras"></param>
        /// <returns></returns>
        public PRODUTOS AdicionarProduto(string nome, string codigoBarras);
    }
}
