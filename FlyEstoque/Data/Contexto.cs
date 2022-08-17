using FlyEstoque.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyEstoque.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<GrupoProduto> GrupoProduto { get; set; }
        public DbSet<Movimento> Movimento { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=1473;Persist Security Info=True;User ID=sa;Initial Catalog=FlyEstoque;Data Source=localhost");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ModelBilder Produto
            modelBuilder.Entity<Produto>()
                     .Property(x => x.EstoqueMax)
                     .HasPrecision(18, 2);
            modelBuilder.Entity<Produto>()
                     .Property(x => x.EstoqueMin)
                     .HasPrecision(18, 2);
            modelBuilder.Entity<Produto>()
                     .Property(x => x.SaldoAtual)
                     .HasPrecision(18, 2);

            //Modelbilder Movimento
            modelBuilder.Entity<Movimento>()
                    .Property(x => x.Quantidade)
                    .HasPrecision(18, 2);

        }
    }
}
