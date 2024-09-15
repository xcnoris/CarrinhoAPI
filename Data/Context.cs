using CarrinhoAPI.Data.Map;
using CarrinhoAPI.Models;
using CarrinhoAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarrinhoAPI.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<CarrinhoModel> Carrinhos { get; set; }
        public DbSet<CongregacaoModel> Congregacoes { get; set; }
        public DbSet<EntidadeModel> Entidades { get; set; }
        public DbSet<LocalPregacaoModel> Locais_Pregacao { get; set; }
        public DbSet<SituacaoAgendamentoModel> Situacao_Agendamento { get; set; }
        public DbSet<CategoriaAgendamentoModel> Categoria_Agendamento { get; set; }
        public DbSet<AgendamentoModel> Agendamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CongregacaoMap());
            modelBuilder.ApplyConfiguration(new CarrinhoMap());
            modelBuilder.ApplyConfiguration(new EntidadeMap());
            modelBuilder.ApplyConfiguration(new LocalPregacaoMap());
            modelBuilder.ApplyConfiguration(new SituacaoAgendamentoMap());
            modelBuilder.ApplyConfiguration(new CategoriaAgendamentoMap());
            modelBuilder.ApplyConfiguration(new AgendamentoMap());
          //  modelBuilder.ApplyConfiguration(new  AgendamentoMap());

            base.OnModelCreating(modelBuilder); // Chama o método base para garantir a configuração padrão do Identity

            // Outras configurações pode ser adicionadas aqui.
        }
    }
}
