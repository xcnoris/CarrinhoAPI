﻿// <auto-generated />
using System;
using CarrinhoAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarrinhoAPI.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240915152101_AddColunDataCriacaoCateriaAgendamento")]
    partial class AddColunDataCriacaoCateriaAgendamento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarrinhoAPI.Models.AgendamentoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarrinhoId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAgendamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Data_Atualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Data_Criacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("EntidadeId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Hora1")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("Hora2")
                        .HasColumnType("time");

                    b.Property<int>("LocalPregacaoId")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("EntidadeId");

                    b.HasIndex("LocalPregacaoId");

                    b.HasIndex("SituacaoId");

                    b.HasIndex("CarrinhoId", "LocalPregacaoId", "DataAgendamento", "Hora1", "Hora2")
                        .IsUnique();

                    b.ToTable("Agendamento");
                });

            modelBuilder.Entity("CarrinhoAPI.Models.CategoriaAgendamentoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Categoria_Agendamento");
                });

            modelBuilder.Entity("CarrinhoAPI.Models.CongregacaoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("CarrinhoAPI.Models.SituacaoAgendamentoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Situacao_Agendamento");
                });

            modelBuilder.Entity("CarrinhoAPI.Models.AgendamentoModel", b =>
                {
                    b.HasOne("CarrinhoAPI.Models.Enums.CarrinhoModel", "Carrinho")
                        .WithMany()
                        .HasForeignKey("CarrinhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarrinhoAPI.Models.CategoriaAgendamentoModel", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarrinhoAPI.Models.EntidadeModel", "Entidade")
                        .WithMany()
                        .HasForeignKey("EntidadeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CarrinhoAPI.Models.LocalPregacaoModel", "LocalPregacao")
                        .WithMany()
                        .HasForeignKey("LocalPregacaoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CarrinhoAPI.Models.SituacaoAgendamentoModel", "Situacao")
                        .WithMany()
                        .HasForeignKey("SituacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrinho");

                    b.Navigation("Categoria");

                    b.Navigation("Entidade");

                    b.Navigation("LocalPregacao");

                    b.Navigation("Situacao");
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
