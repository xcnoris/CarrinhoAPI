using CarrinhoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoAPI.Data.Map
{
    public class CarrinhoMap : IEntityTypeConfiguration<CarrinhoModel>
    {
        public void Configure(EntityTypeBuilder<CarrinhoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Codigo_Carrinho).IsRequired();
            builder.Property(x => x.Situacao).IsRequired();
            builder.Property(x => x.CongregacaoId).IsRequired();
            builder.HasOne(x => x.Congregacao);


        }
    }
}
