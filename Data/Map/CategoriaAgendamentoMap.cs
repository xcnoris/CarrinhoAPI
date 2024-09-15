using CarrinhoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoAPI.Data.Map
{
    public class CategoriaAgendamentoMap : IEntityTypeConfiguration<CategoriaAgendamentoModel>
    {
        public void Configure(EntityTypeBuilder<CategoriaAgendamentoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome);
            builder.Property(x => x.Data_Criacao);
            builder.Property(x => x.Data_Atualizacao);
        }
    }
}
