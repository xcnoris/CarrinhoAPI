using CarrinhoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoAPI.Data.Map
{
    public class CongregacaoMap : IEntityTypeConfiguration<CongregacaoModel>
    {
        public void Configure(EntityTypeBuilder<CongregacaoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Situacao).IsRequired();


        }
    }
}
