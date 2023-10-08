﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20231008225944_AdicionarTodasListaTarefas")]
    partial class AdicionarTodasListaTarefas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("API.Cargo", b =>
                {
                    b.Property<int>("CargoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("CargoId");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("API.Comentario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TarefaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Texto")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("API.Grupo", b =>
                {
                    b.Property<int>("GrupoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("GrupoId");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("API.Prioridade", b =>
                {
                    b.Property<int>("PrioridadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("PrioridadeId");

                    b.ToTable("Prioridades");
                });

            modelBuilder.Entity("API.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("StatusId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("API.Tarefa", b =>
                {
                    b.Property<int>("TarefaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Corpo")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PrioridadeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StatusId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TarefaId");

                    b.HasIndex("PrioridadeId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Tarefas");

                    b.HasDiscriminator<string>("Tipo").HasValue("Tarefa");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("API.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Logado")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("Tipo").HasValue("Usuario");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("API.TarefaAtividade", b =>
                {
                    b.HasBaseType("API.Tarefa");

                    b.HasDiscriminator().HasValue("Atividade");
                });

            modelBuilder.Entity("API.TarefaProjeto", b =>
                {
                    b.HasBaseType("API.Tarefa");

                    b.HasDiscriminator().HasValue("Projeto");
                });

            modelBuilder.Entity("API.UsuarioAdmin", b =>
                {
                    b.HasBaseType("API.Usuario");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("API.UsuarioGerencial", b =>
                {
                    b.HasBaseType("API.Usuario");

                    b.Property<int?>("GrupoId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("GrupoId");

                    b.HasDiscriminator().HasValue("Gerencial");
                });

            modelBuilder.Entity("API.UsuarioOperacional", b =>
                {
                    b.HasBaseType("API.Usuario");

                    b.Property<int?>("GrupoId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("GrupoId");

                    b.ToTable("Usuarios", t =>
                        {
                            t.Property("GrupoId")
                                .HasColumnName("UsuarioOperacional_GrupoId");
                        });

                    b.HasDiscriminator().HasValue("Operacional");
                });

            modelBuilder.Entity("API.Tarefa", b =>
                {
                    b.HasOne("API.Prioridade", "Prioridade")
                        .WithMany()
                        .HasForeignKey("PrioridadeId");

                    b.HasOne("API.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("API.Usuario", "Usuario")
                        .WithMany("Tarefas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prioridade");

                    b.Navigation("Status");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("API.UsuarioGerencial", b =>
                {
                    b.HasOne("API.Grupo", "Grupo")
                        .WithMany()
                        .HasForeignKey("GrupoId");

                    b.Navigation("Grupo");
                });

            modelBuilder.Entity("API.UsuarioOperacional", b =>
                {
                    b.HasOne("API.Grupo", "Grupo")
                        .WithMany()
                        .HasForeignKey("GrupoId");

                    b.Navigation("Grupo");
                });

            modelBuilder.Entity("API.Usuario", b =>
                {
                    b.Navigation("Tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}
