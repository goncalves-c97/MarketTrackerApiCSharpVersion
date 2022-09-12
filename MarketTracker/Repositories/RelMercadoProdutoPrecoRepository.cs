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

        public bool AdicionarNovaRelacaoMercadoProdutoPreco(int mercadoID, int produtoID, decimal preco)
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
            return true;
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
    }
}
