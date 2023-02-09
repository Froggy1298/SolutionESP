using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjetHelper.Models;

public partial class A22Sda2031887Context : DbContext
{
    public A22Sda2031887Context()
    {
    }

    public A22Sda2031887Context(DbContextOptions<A22Sda2031887Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Tbldepartement> Tbldepartements { get; set; }

    public virtual DbSet<Tblfacture> Tblfactures { get; set; }

    public virtual DbSet<Tblproduit> Tblproduits { get; set; }

    public virtual DbSet<Tblproduitfacture> Tblproduitfactures { get; set; }

    public virtual DbSet<Tblrapporthebdomadaire> Tblrapporthebdomadaires { get; set; }

    public virtual DbSet<Tblrapportmensuel> Tblrapportmensuels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=sql.decinfo-cchic.ca;port=33306;database=a22_sda_2031887;uid=dev-2031887;pwd=Info2020", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Tbldepartement>(entity =>
        {
            entity.HasKey(e => e.IdDepartement).HasName("PRIMARY");

            entity.ToTable("tbldepartement");

            entity.Property(e => e.IdDepartement).HasColumnName("idDepartement");
            entity.Property(e => e.NomDepartement)
                .HasMaxLength(100)
                .HasColumnName("nomDepartement");
        });

        modelBuilder.Entity<Tblfacture>(entity =>
        {
            entity.HasKey(e => e.IdFacture).HasName("PRIMARY");

            entity.ToTable("tblfacture");

            entity.Property(e => e.IdFacture).HasColumnName("idFacture");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Etat)
                .HasMaxLength(50)
                .HasColumnName("etat");
            entity.Property(e => e.ModePaiment)
                .HasMaxLength(50)
                .HasColumnName("modePaiment");
            entity.Property(e => e.Tpspercent)
                .HasPrecision(6, 2)
                .HasColumnName("TPSPercent");
            entity.Property(e => e.Tvqpercent)
                .HasPrecision(6, 2)
                .HasColumnName("TVQPercent");
        });

        modelBuilder.Entity<Tblproduit>(entity =>
        {
            entity.HasKey(e => e.IdProduit).HasName("PRIMARY");

            entity.ToTable("tblproduit");

            entity.HasIndex(e => e.Cup, "CUP_UNIQUE").IsUnique();

            entity.HasIndex(e => e.IdDepartement, "idDepartement_idx");

            entity.Property(e => e.IdProduit).HasColumnName("idProduit");
            entity.Property(e => e.Cup).HasColumnName("CUP");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .HasColumnName("description");
            entity.Property(e => e.IdDepartement).HasColumnName("idDepartement");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Prix)
                .HasPrecision(6, 2)
                .HasColumnName("prix");
            entity.Property(e => e.QteInventaire).HasColumnName("qteInventaire");
            entity.Property(e => e.Tps).HasColumnName("TPS");
            entity.Property(e => e.Tvq).HasColumnName("TVQ");
            entity.Property(e => e.VentePoids).HasColumnName("ventePoids");

            entity.HasOne(d => d.IdDepartementNavigation).WithMany(p => p.Tblproduits)
                .HasForeignKey(d => d.IdDepartement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idDepartement");
        });

        modelBuilder.Entity<Tblproduitfacture>(entity =>
        {
            entity.HasKey(e => e.IdProduitFacture).HasName("PRIMARY");

            entity.ToTable("tblproduitfacture");

            entity.HasIndex(e => e.IdFacture, "idFacture_idx");

            entity.HasIndex(e => e.IdProduit, "idProduit_idx");

            entity.Property(e => e.IdProduitFacture).HasColumnName("idProduitFacture");
            entity.Property(e => e.CoutTotal)
                .HasPrecision(6, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("coutTotal");
            entity.Property(e => e.IdFacture).HasColumnName("idFacture");
            entity.Property(e => e.IdProduit).HasColumnName("idProduit");
            entity.Property(e => e.NbFoisCommandee)
                .HasDefaultValueSql("'0'")
                .HasColumnName("nbFoisCommandee");

            entity.HasOne(d => d.IdFactureNavigation).WithMany(p => p.Tblproduitfactures)
                .HasForeignKey(d => d.IdFacture)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idFacture");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.Tblproduitfactures)
                .HasForeignKey(d => d.IdProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idProduit");
        });

        modelBuilder.Entity<Tblrapporthebdomadaire>(entity =>
        {
            entity.HasKey(e => e.IdRapportHebdomadaire).HasName("PRIMARY");

            entity.ToTable("tblrapporthebdomadaire");

            entity.Property(e => e.IdRapportHebdomadaire).HasColumnName("idRapportHebdomadaire");
            entity.Property(e => e.DateDebut)
                .HasColumnType("datetime")
                .HasColumnName("dateDebut");
            entity.Property(e => e.DateFin)
                .HasColumnType("datetime")
                .HasColumnName("dateFin");
            entity.Property(e => e.MoyNbTransactionParJour)
                .HasPrecision(6, 2)
                .HasColumnName("moyNbTransactionParJour");
            entity.Property(e => e.MoyPrixTransaction)
                .HasPrecision(6, 2)
                .HasColumnName("moyPrixTransaction");
            entity.Property(e => e.MoyVenteTotalParJour)
                .HasPrecision(6, 2)
                .HasColumnName("moyVenteTotalParJour");
        });

        modelBuilder.Entity<Tblrapportmensuel>(entity =>
        {
            entity.HasKey(e => e.IdRapportMensuel).HasName("PRIMARY");

            entity.ToTable("tblrapportmensuel");

            entity.Property(e => e.IdRapportMensuel).HasColumnName("idRapportMensuel");
            entity.Property(e => e.DateDebut)
                .HasColumnType("datetime")
                .HasColumnName("dateDebut");
            entity.Property(e => e.DateFin)
                .HasColumnType("datetime")
                .HasColumnName("dateFin");
            entity.Property(e => e.NbTransactionTotal).HasColumnName("nbTransactionTotal");
            entity.Property(e => e.SommeVente)
                .HasPrecision(6, 2)
                .HasColumnName("sommeVente");
            entity.Property(e => e.ValeurMoyTransaction)
                .HasPrecision(6, 2)
                .HasColumnName("valeurMoyTransaction");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
