using CarrinhoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoAPI.Data.Map
{
    public class SituacaoAgendamentoMap : IEntityTypeConfiguration<SituacaoAgendamentoModel>
    {
        public void Configure(EntityTypeBuilder<SituacaoAgendamentoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome);
        }
    }
}
