using CarrinhoAPI.Models;
using CarrinhoAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarrinhoAPI.Data.Map
{
    public class EntidadeMap : IEntityTypeConfiguration<EntidadeModel>
    {
        public void Configure(EntityTypeBuilder<EntidadeModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CPF).HasMaxLength(11);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(250);
            builder.Property(x => x.CEP);
            builder.Property(x => x.Endereco);
            builder.Property(x => x.Endereco_Numero);
            builder.Property(x => x.Endereco_Complemento);
            builder.Property(x => x.Bairro);
            builder.Property(x => x.Cidade_Nome);
            builder.Property(x => x.UF).HasMaxLength(2);
            builder.Property(x => x.DDD_Celular).HasMaxLength(2);
            builder.Property(x => x.Celular).HasMaxLength(9);
            builder.Property(x => x.Sexo).HasMaxLength(1);
            builder.Property(x => x.DataNascimento);
            builder.Property(x => x.Email);
            builder.Property(x => x.CongregacaoId).IsRequired();
            builder.HasOne(x => x.Congregacao);
            builder.Property(x => x.Data_Cadastro).IsRequired();


        }
    }
}
