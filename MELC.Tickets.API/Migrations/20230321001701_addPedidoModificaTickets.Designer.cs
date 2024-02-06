﻿// <auto-generated />
using System;
using MELC.Main.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MELC.Main.API.Migrations
{
    [DbContext(typeof(MelcContext))]
    [Migration("20230321001701_addPedidoModificaTickets")]
    partial class addPedidoModificaTickets
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MELC.Main.API.Models.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Cliente")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CriadoPorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataDeEntrega")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(300)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UltimaAtualizacao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.ToTable("Pedidos", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CriadoPorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime>("UltimaAtualizacao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("Tickets", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.TicketControleUsuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TicketId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("TicketControleUsuarios", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.TicketItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CriadoPorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<double>("Quantidade")
                        .HasColumnType("float");

                    b.Property<Guid>("TicketId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketItems", (string)null);
                });

            modelBuilder.Entity("MELC.Main.API.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("TicketUser", b =>
                {
                    b.Property<Guid>("TicketsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuariosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TicketsId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("TicketUser");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Pedido", b =>
                {
                    b.HasOne("MELC.Main.API.Models.User", "CriadoPor")
                        .WithMany("Pedidos")
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CriadoPor");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Ticket", b =>
                {
                    b.HasOne("MELC.Main.API.Models.Pedido", "Pedido")
                        .WithMany("Tickets")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("MELC.Main.API.Models.TicketControleUsuario", b =>
                {
                    b.HasOne("MELC.Main.API.Models.Ticket", "Ticket")
                        .WithMany("ControleUsuarios")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.User", "Usuario")
                        .WithMany("TicketControleUsuarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MELC.Main.API.Models.TicketItem", b =>
                {
                    b.HasOne("MELC.Main.API.Models.User", "CriadoPor")
                        .WithMany("TicketItems")
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.Ticket", "Ticket")
                        .WithMany("TicketItems")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CriadoPor");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("TicketUser", b =>
                {
                    b.HasOne("MELC.Main.API.Models.Ticket", null)
                        .WithMany()
                        .HasForeignKey("TicketsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MELC.Main.API.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MELC.Main.API.Models.Pedido", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("MELC.Main.API.Models.Ticket", b =>
                {
                    b.Navigation("ControleUsuarios");

                    b.Navigation("TicketItems");
                });

            modelBuilder.Entity("MELC.Main.API.Models.User", b =>
                {
                    b.Navigation("Pedidos");

                    b.Navigation("TicketControleUsuarios");

                    b.Navigation("TicketItems");
                });
#pragma warning restore 612, 618
        }
    }
}