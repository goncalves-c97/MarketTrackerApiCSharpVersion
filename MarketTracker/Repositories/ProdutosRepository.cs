using MarketTracker.Interfaces;
using MarketTracker.Models;
using Microsoft.EntityFrameworkCore;
using SmartAssistInforTecApi.Data;
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
                .FirstOrDefault(x => x.NOME.ToUpper() == nome.ToUpper() || (x.COD_BARRAS != null && x.COD_BARRAS == codigoBarras));

            if (produtoDatabase != null)
                return produtoDatabase;

            produtoDatabase = new PRODUTOS { NOME = nome, COD_BARRAS = codigoBarras };

            _context.Add(produtoDatabase);
            _context.SaveChanges();

            return produtoDatabase;
        }
    }
}
