using MarketTracker.Models;

namespace MarketTracker.Interfaces
{
    public interface IProdutos
    {
        /// <summary>
        /// Adiciona o produto e retorna o produto inserido no banco, caso já exista o produto, este é retornado
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="codigoBarras"></param>
        /// <returns></returns>
        public PRODUTOS AdicionarProduto(string nome, string codigoBarras);
    }
}
