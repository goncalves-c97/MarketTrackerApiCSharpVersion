using MarketTracker.Interfaces;
using MarketTracker.Models;
using Microsoft.EntityFrameworkCore;
using SmartAssistInforTecApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketTracker.Repositories
{
    public class RelMercadoProdutoPrecoRepository : IRelMercadoProdutoPreco
    {
        private readonly AppDbContext _context;

        public RelMercadoProdutoPrecoRepository(AppDbContext context)
        {
            _context = context;
        }

        public REL_MERCADO_PRODUTO_PRECO AdicionarNovaRelacaoMercadoProdutoPreco(int mercadoID, int produtoID, decimal preco)
        {
            REL_MERCADO_PRODUTO_PRECO relacaoMercadoProdutoPreco = new REL_MERCADO_PRODUTO_PRECO
            {
                DATA_HORA_REGISTRO = DateTime.Now,
                ID_MERCADO = mercadoID,
                ID_PRODUTO = produtoID,
                PRECO = preco 
            };

            _context.Add(relacaoMercadoProdutoPreco);
            _context.SaveChanges();

            return relacaoMercadoProdutoPreco;
        }

        public List<REL_MERCADO_PRODUTO_PRECO> BuscarPorIdsProdutos(List<int> idsProdutos)
        {
            return _context.Precos
                .AsNoTracking()
                .Where(x => idsProdutos.Any(i => i == x.ID_PRODUTO))
                .Include(x => x.MERCADO)
                .ToList();
        }

        public List<REL_MERCADO_PRODUTO_PRECO> BuscarPorTexto(string textoDeBusca)
        {
            return _context.Precos
                .AsNoTracking()
                .Include(x => x.MERCADO)
                .Include(x => x.PRODUTO)
                .Where(x => x.PRODUTO.NOME.ToUpper().Contains(textoDeBusca))
                .ToList();
        }

        public List<REL_MERCADO_PRODUTO_PRECO> ListarTodos()
        {
            return _context.Precos
                .AsNoTracking()
                .Include(x => x.MERCADO)
                .Include(x => x.PRODUTO)
                .ToList();
        }
    }
}
