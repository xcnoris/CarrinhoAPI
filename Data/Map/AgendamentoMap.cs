using CarrinhoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoAPI.Data.Map
{
    public class AgendamentoMap : IEntityTypeConfiguration<AgendamentoModel>
    {
        public void Configure(EntityTypeBuilder<AgendamentoModel> builder)
        {
            // Definir chave primária
            builder.HasKey(a => a.Id);

            // Definir campo obrigatório para SituacaoId e relacionamento
            builder.Property(a => a.SituacaoId).IsRequired();
            builder.HasOne(a => a.Situacao)
                .WithMany() // ou .WithOne(), dependendo do relacionamento
                .HasForeignKey(a => a.SituacaoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Definir campo obrigatório para CategoriaId e relacionamento
            builder.Property(a => a.CategoriaId).IsRequired();
            builder.HasOne(a => a.Categoria);

            // Definir campo obrigatório para CarrinhoId e relacionamento
            builder.Property(a => a.CarrinhoId).IsRequired();
            builder.HasOne(a => a.Carrinho);

            // Definir campo obrigatório para DataAgendamento
            builder.Property(a => a.DataAgendamento).IsRequired();

            // Definir campo obrigatório para Hora1 e Hora2
            builder.Property(a => a.Hora1)
                   .IsRequired();
            builder.Property(a => a.Hora2)
                   .IsRequired();

            // Definir campo obrigatório para LocalPregacaoId e relacionamento
            builder.Property(a => a.LocalPregacaoId).IsRequired();
            builder.HasOne(a => a.LocalPregacao).WithMany().HasForeignKey(a => a.LocalPregacaoId).OnDelete(DeleteBehavior.Restrict);

            // Definir Data de Criação
            builder.Property(a => a.Data_Criacao).IsRequired();

            // Definir Data de Atualização (pode ser nula)
            builder.Property(a => a.Data_Atualizacao).IsRequired(false);

            // Definir campo obrigatório para EntidadeId e relacionamento
            builder.Property(a => a.EntidadeId)
                   .IsRequired();
            builder.HasOne(a => a.Entidade)
                   .WithMany()
                   .HasForeignKey(a => a.EntidadeId)
                   .OnDelete(DeleteBehavior.Restrict);



            // Definir índice único para garantir que CarrinhoId e LocalPregacaoId sejam únicos por horário e dia
            builder.HasIndex(a => new { a.CarrinhoId, a.LocalPregacaoId, a.DataAgendamento, a.Hora1, a.Hora2 })
                   .IsUnique();

        }

    }
}
