using MarketTracker.Interfaces;
using MarketTracker.Models;
using Microsoft.EntityFrameworkCore;
using SmartAssistInforTecApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketTracker.Repositories
{
    public class ProdutosRepository : IProdutos
    {
        private readonly AppDbContext _context;

        public ProdutosRepository(AppDbContext context)
        {
            _context = context;
        }

        public PRODUTOS AdicionarProduto(string nome, string codigoBarras)
        {
            PRODUTOS produtoDatabase = _context.Produtos
                .AsNoTracking()
                .FirstOrDefault(x => x.NOME.ToUpper() == nome.ToUpper());

            if (produtoDatabase != null)
                throw new Exception("Já existe um produto com o nome informado.");

            produtoDatabase = new PRODUTOS 
            { 
                NOME = nome,
                COD_BARRAS = codigoBarras
            };

            _context.Add(produtoDatabase);
            _context.SaveChanges();

            return produtoDatabase;
        }

        public List<PRODUTOS> BuscaProdutosPorNome(string nomeProduto)
        {
            return _context.Produtos
                .AsNoTracking()
                .Where(x => x.NOME.ToUpper().Contains(nomeProduto.ToUpper()))
                .ToList();
        }

        public PRODUTOS GetByProdutoByID(int produtoID)
        {
            return _context.Produtos
                .AsNoTracking()
                .FirstOrDefault(x => x.ID == produtoID);
        }

        public List<PRODUTOS> ListarProdutos()
        {
            return _context.Produtos
                .OrderBy(x => x.NOME)
                .ToList();
        }
    }
}
