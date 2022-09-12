using MarketTracker.Models;
using Microsoft.EntityFrameworkCore;


namespace SmartAssistInforTecApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            this.Database.SetCommandTimeout(3600);
        }

        public DbSet<MERCADOS> Mercados { get; set; }
        public DbSet<PRODUTOS> Produtos { get; set; }
        public DbSet<REL_MERCADO_PRODUTO_PRECO> Precos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<REL_MERCADO_PRODUTO_PRECO>().HasOne(item => item.MERCADO).WithMany().HasForeignKey(item => item.ID_MERCADO);
            modelBuilder.Entity<REL_MERCADO_PRODUTO_PRECO>().HasOne(item => item.PRODUTO).WithMany().HasForeignKey(item => item.ID_PRODUTO);
        }
    }
}


