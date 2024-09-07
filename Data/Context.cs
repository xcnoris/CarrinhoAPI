using CarrinhoAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarrinhoAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        //public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Congregacao> Congregacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Chama o método base para garantir a configuração padrão do Identity


            // Outras configurações pode ser adicionadas aqui.
        }
    }
}
