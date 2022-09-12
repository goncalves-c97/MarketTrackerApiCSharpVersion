using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarketTracker.Models
{
    public static class LeitorDeProdutos
    {
        public static List<REL_MERCADO_PRODUTO_PRECO> GetProdutosCarrefourByHtmlString(string htmlPage)
        {
            string htmlSingleLine = htmlPage.Replace("\r", "").Replace("\n", "").Replace("\t", "");

            TimeSpan timeoutRegex = TimeSpan.FromSeconds(1);

            Regex nomeProduto = new Regex("(?<=<h\\d data-testid=\"productSummaryTitle\" class=\"[A-Z]+[0-9A-Z_ -]*\">)[^<]+", RegexOptions.IgnoreCase, timeoutRegex);
            Regex precoProduto = new Regex("(?<=<div data-testid=\"offerPrice\" class=\"[A-Z]+[0-9A-Z_ -]*\">R\\$&nbsp;)[^<]+", RegexOptions.IgnoreCase, timeoutRegex);

            MatchCollection nomesProdutos = nomeProduto.Matches(htmlSingleLine);
            MatchCollection precosProdutos = precoProduto.Matches(htmlSingleLine);

            int qtdProdutos = nomesProdutos.Count;

            List<REL_MERCADO_PRODUTO_PRECO> produtos = new List<REL_MERCADO_PRODUTO_PRECO>();

            for (int i = 0; i < qtdProdutos; i++)
            {
                produtos.Add(new REL_MERCADO_PRODUTO_PRECO
                {
                    ID_MERCADO = 1,
                    PRODUTO = new PRODUTOS { NOME = nomesProdutos[0].Value, COD_BARRAS = null },
                    DATA_HORA_REGISTRO = DateTime.Now,
                    PRECO = decimal.Parse(precosProdutos[0].Value, new CultureInfo("pt-BR"))
                });
            }

            return produtos;
        }
    }
}
