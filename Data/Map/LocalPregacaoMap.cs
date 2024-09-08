using CarrinhoAPI.Models;
using CarrinhoAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoAPI.Data.Map
{
    public class LocalPregacaoMap : IEntityTypeConfiguration<LocalPregacaoModel>
    {
        public void Configure(EntityTypeBuilder<LocalPregacaoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Descricao).HasMaxLength(500);
            builder.Property(x => x.Endereco).IsRequired();
            builder.Property(x => x.Endereco).IsRequired();
            builder.Property(x => x.Complemento);
            builder.Property(x => x.Bairro).IsRequired();
            builder.Property(x => x.Cidade).IsRequired();
            builder.Property(x => x.UF).HasMaxLength(2).IsRequired();
            builder.Property(x => x.Situacao).IsRequired().HasMaxLength(1);
            builder.Property(x => x.CongregacaoId).IsRequired();
            builder.HasOne(x => x.Congregacao);
            builder.Property(x => x.Data_Cadastro).IsRequired();


        }
    }
}
