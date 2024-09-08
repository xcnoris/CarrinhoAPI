﻿// <auto-generated />
using System;
using CarrinhoAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarrinhoAPI.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarrinhoAPI.Models.CongregacaoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Congregacoes");
                });

            modelBuilder.Entity("CarrinhoAPI.Models.EntidadeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CPF")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Celular")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Cidade_Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CongregacaoId")
                        .HasColumnType("int");

                    b.Property<string>("DDD_Celular")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Data_Cadastro")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Endereco_Complemento")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Endereco_Numero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Sexo")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("UF")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("CongregacaoId");

                    b.ToTable("Entidades");
                });

            modelBuilder.Entity("CarrinhoAPI.Models.Enums.CarrinhoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Codigo_Carrinho")
                        .HasColumnType("int");

                    b.Property<int>("CongregacaoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CongregacaoId");

                    b.ToTable("Carrinhos");
                });

            modelBuilder.Entity("CarrinhoAPI.Models.LocalPregacaoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complemento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CongregacaoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data_Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Situacao")
                        .HasMaxLength(1)
                        .HasColumnType("int");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("CongregacaoId");

                    b.ToTable("Locais_Pregacao");
                });

            modelBuilder.Entity("CarrinhoAPI.Models.EntidadeModel", b =>
                {
                    b.HasOne("CarrinhoAPI.Models.CongregacaoModel", "Congregacao")
                        .WithMany()
                        .HasForeignKey("CongregacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Congregacao");
                });

            modelBuilder.Entity("CarrinhoAPI.Models.Enums.CarrinhoModel", b =>
                {
                    b.HasOne("CarrinhoAPI.Models.CongregacaoModel", "Congregacao")
                        .WithMany()
                        .HasForeignKey("CongregacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Congregacao");
                });

            modelBuilder.Entity("CarrinhoAPI.Models.LocalPregacaoModel", b =>
                {
                    b.HasOne("CarrinhoAPI.Models.CongregacaoModel", "Congregacao")
                        .WithMany()
                        .HasForeignKey("CongregacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Congregacao");
                });
#pragma warning restore 612, 618
        }
    }
}
