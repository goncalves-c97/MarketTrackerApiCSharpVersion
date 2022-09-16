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

        public MERCADOS AdicionarMercado(string nome)
        {
            if (_context.Mercados.AsNoTracking().FirstOrDefault(x => x.NOME == nome) != null)
                throw new Exception("Já existe um mercado com o nome informado.");

            MERCADOS novoMercado = new MERCADOS { NOME = nome };
            _context.Mercados.Add(novoMercado);
            _context.SaveChanges();

            return novoMercado;
        }

        public List<MERCADOS> BuscaMercadosPorNome(string nomeMercado)
        {
            return _context.Mercados
                .AsNoTracking()
                .Where(x => x.NOME.ToUpper().Contains(nomeMercado.ToUpper()))
                .ToList();
        }

        public MERCADOS GetByMercadoByID(int mercadoID)
        {
            return _context.Mercados
                .AsNoTracking()
                .FirstOrDefault(x => x.ID == mercadoID);
        }
    }
}
