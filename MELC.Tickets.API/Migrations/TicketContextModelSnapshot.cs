﻿// <auto-generated />
using System;
using MELC.Main.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MELC.Main.API.Migrations
{
    [DbContext(typeof(MelcContext))]
    partial class TicketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MELC.Main.API.Models.ArquivoDesenho", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("CaminhoArquivo")
                        .IsRequired()
                        .HasColumnType("varchar(600)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DesenhoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Extensao")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NomeArquivo")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("DesenhoId");

                    b.ToTable("ArquivosDesenho", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Cnpj")
                        .HasMaxLength(18)
                        .IsUnicode(true)
                        .HasColumnType("varchar(18)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EnderecoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.Desenho", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Conjunto")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CriadoPorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<double?>("Impostos")
                        .HasColumnType("float");

                    b.Property<double?>("Lucro")
                        .HasColumnType("float");

                    b.Property<int?>("NumeroConjunto")
                        .HasColumnType("int");

                    b.Property<int>("NumeroDesenho")
                        .HasColumnType("int");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime>("UltimaAtualizacao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.HasIndex("PedidoId");

                    b.ToTable("Desenhos", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.DesenhoServico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CriadoPorId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DesenhoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Horas")
                        .HasColumnType("int");

                    b.Property<int?>("Minutos")
                        .HasColumnType("int");

                    b.Property<Guid>("TipoServicoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.HasIndex("DesenhoId");

                    b.HasIndex("TipoServicoId");

                    b.ToTable("DesenhoServicos", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Enderecos", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.Faturamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("CaminhoArquivo")
                        .IsRequired()
                        .HasColumnType("varchar(600)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CriadoPorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Extensao")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NomeArquivo")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Pecas")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.HasIndex("PedidoId");

                    b.ToTable("Faturamentos", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.FreteDesenho", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CriadoPorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DesenhoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.HasIndex("DesenhoId");

                    b.ToTable("FretesDesenhos", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.Material", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<double>("Densidade")
                        .HasColumnType("float");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Materiais", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.MaterialDesenho", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CriadoPorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DesenhoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Peso")
                        .HasColumnType("float");

                    b.Property<double>("Quantidade")
                        .HasColumnType("float");

                    b.Property<Guid>("SolidoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.HasIndex("DesenhoId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("SolidoId");

                    b.ToTable("MateriaisDesenhos", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.PecaNormalizada", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CriadoPorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DesenhoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Preco")
                        .HasColumnType("float");

                    b.Property<double>("Quantidade")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.HasIndex("DesenhoId");

                    b.ToTable("PecasNormalizadas", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CriadoPorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataDeEntrega")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(300)");

                    b.Property<int>("NumeroPedido")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UltimaAtualizacao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("CriadoPorId");

                    b.ToTable("Pedidos", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.Percentuais", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Impostos")
                        .HasColumnType("float");

                    b.Property<double?>("Lucro")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Percentuais", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.Solido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<double?>("Altura")
                        .HasColumnType("float");

                    b.Property<double>("Comprimento")
                        .HasColumnType("float");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Diametro")
                        .HasColumnType("float");

                    b.Property<double?>("Expessura")
                        .HasColumnType("float");

                    b.Property<double?>("ExpessuraSuperior")
                        .HasColumnType("float");

                    b.Property<double?>("Largura")
                        .HasColumnType("float");

                    b.Property<int>("TipoSolido")
                        .HasColumnType("int");

                    b.Property<double>("Volume")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Solidos", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.TipoServico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Servico")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("TiposServicos", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.ArquivoDesenho", b =>
                {
                    b.HasOne("MELC.Main.API.Models.Desenho", "Desenho")
                        .WithMany("Arquivos")
                        .HasForeignKey("DesenhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Desenho");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Cliente", b =>
                {
                    b.HasOne("MELC.Main.API.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Desenho", b =>
                {
                    b.HasOne("MELC.Main.API.Models.User", "CriadoPor")
                        .WithMany("Desenhos")
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.Pedido", "Pedido")
                        .WithMany("Desenhos")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CriadoPor");

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("MELC.Main.API.Models.DesenhoServico", b =>
                {
                    b.HasOne("MELC.Main.API.Models.User", "CriadoPor")
                        .WithMany("DesenhoServicos")
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.Desenho", "Desenho")
                        .WithMany("DesenhoServicos")
                        .HasForeignKey("DesenhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.TipoServico", "TipoServico")
                        .WithMany("DesenhoServicos")
                        .HasForeignKey("TipoServicoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CriadoPor");

                    b.Navigation("Desenho");

                    b.Navigation("TipoServico");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Faturamento", b =>
                {
                    b.HasOne("MELC.Main.API.Models.User", "CriadoPor")
                        .WithMany("Faturamentos")
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.Pedido", "Pedido")
                        .WithMany("Faturamentos")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CriadoPor");

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("MELC.Main.API.Models.FreteDesenho", b =>
                {
                    b.HasOne("MELC.Main.API.Models.User", "CriadoPor")
                        .WithMany("FretesDesenhos")
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.Desenho", "Desenho")
                        .WithMany("FretesDesenhos")
                        .HasForeignKey("DesenhoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CriadoPor");

                    b.Navigation("Desenho");
                });

            modelBuilder.Entity("MELC.Main.API.Models.MaterialDesenho", b =>
                {
                    b.HasOne("MELC.Main.API.Models.User", "CriadoPor")
                        .WithMany("MaterialDesenho")
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.Desenho", "Desenho")
                        .WithMany("MateriaisDesenhos")
                        .HasForeignKey("DesenhoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.Material", "Material")
                        .WithMany("MateriaisDesenhos")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.Solido", "Solido")
                        .WithMany()
                        .HasForeignKey("SolidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CriadoPor");

                    b.Navigation("Desenho");

                    b.Navigation("Material");

                    b.Navigation("Solido");
                });

            modelBuilder.Entity("MELC.Main.API.Models.PecaNormalizada", b =>
                {
                    b.HasOne("MELC.Main.API.Models.User", "CriadoPor")
                        .WithMany("PecasNormalizadas")
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.Desenho", "Desenho")
                        .WithMany("PecasNormalizadas")
                        .HasForeignKey("DesenhoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CriadoPor");

                    b.Navigation("Desenho");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Pedido", b =>
                {
                    b.HasOne("MELC.Main.API.Models.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.User", "CriadoPor")
                        .WithMany("Pedidos")
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("CriadoPor");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Cliente", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Desenho", b =>
                {
                    b.Navigation("Arquivos");

                    b.Navigation("DesenhoServicos");

                    b.Navigation("FretesDesenhos");

                    b.Navigation("MateriaisDesenhos");

                    b.Navigation("PecasNormalizadas");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Material", b =>
                {
                    b.Navigation("MateriaisDesenhos");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Pedido", b =>
                {
                    b.Navigation("Desenhos");

                    b.Navigation("Faturamentos");
                });

            modelBuilder.Entity("MELC.Main.API.Models.TipoServico", b =>
                {
                    b.Navigation("DesenhoServicos");
                });

            modelBuilder.Entity("MELC.Main.API.Models.User", b =>
                {
                    b.Navigation("DesenhoServicos");

                    b.Navigation("Desenhos");

                    b.Navigation("Faturamentos");

                    b.Navigation("FretesDesenhos");

                    b.Navigation("MaterialDesenho");

                    b.Navigation("PecasNormalizadas");

                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
