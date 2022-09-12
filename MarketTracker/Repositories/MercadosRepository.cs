using MarketTracker.Interfaces;
using MarketTracker.Models;
using Microsoft.EntityFrameworkCore;
using SmartAssistInforTecApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTracker.Repositories
{
    public class MercadosRepository : IMercados
    {
        private readonly AppDbContext _context;

        public MercadosRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool AdicionarMercado(string nome)
        {
            if (_context.Mercados.AsNoTracking().FirstOrDefault(x => x.NOME == nome) != null)
                throw new Exception("Já existe um mercado com o nome informado.");

            _context.Mercados.Add(new MERCADOS { NOME = nome });
            _context.SaveChanges();

            return true;
        }
    }
}
