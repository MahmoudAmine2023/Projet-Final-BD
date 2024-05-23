using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjetFinal_2073088.Models;

namespace ProjetFinal_2073088.Data
{
    public partial class ProjetFinal_MaillotsContext : DbContext
    {
        public ProjetFinal_MaillotsContext()
        {
        }

        public ProjetFinal_MaillotsContext(DbContextOptions<ProjetFinal_MaillotsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Achat> Achats { get; set; } = null!;
        public virtual DbSet<ArticleCommande> ArticleCommandes { get; set; } = null!;
        public virtual DbSet<Changelog> Changelogs { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Concurrent> Concurrents { get; set; } = null!;
        public virtual DbSet<Courriel> Courriels { get; set; } = null!;
        public virtual DbSet<Fournisseur> Fournisseurs { get; set; } = null!;
        public virtual DbSet<Maillot> Maillots { get; set; } = null!;
        public virtual DbSet<MaillotPremium> MaillotPremia { get; set; } = null!;
        public virtual DbSet<Promotion> Promotions { get; set; } = null!;
        public virtual DbSet<VwMaillotsAvecPromotion> VwMaillotsAvecPromotions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ProjetFinal_Maillots");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achat>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Achats)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Achats_Achat_Client");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.Achats)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("FK_Achats_Achat_PromotionID");
            });

            modelBuilder.Entity<ArticleCommande>(entity =>
            {
                entity.HasOne(d => d.Achat)
                    .WithMany(p => p.ArticleCommandes)
                    .HasForeignKey(d => d.AchatId)
                    .HasConstraintName("FK_Articles_ArticlesCommande_AchatID");
            });

            modelBuilder.Entity<Changelog>(entity =>
            {
                entity.Property(e => e.InstalledOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Concurrent>(entity =>
            {
                entity.HasKey(e => e.ConcurentId)
                    .HasName("PK__Concurre__B12BF83E5B00DC6E");

                entity.HasOne(d => d.Maillot)
                    .WithMany(p => p.Concurrents)
                    .HasForeignKey(d => d.MaillotId)
                    .HasConstraintName("FK_Concurents_Concurrent_MaillotID");
            });

            modelBuilder.Entity<Maillot>(entity =>
            {
                entity.Property(e => e.Identifiant).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.Maillots)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("FK_Maillots_Promotions");
            });

            modelBuilder.Entity<MaillotPremium>(entity =>
            {
                entity.Property(e => e.MaillotPremiumId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Maillot)
                    .WithMany()
                    .HasForeignKey(d => d.MaillotId)
                    .HasConstraintName("FK_MaillotPremium_MaillotID");
            });

            modelBuilder.Entity<VwMaillotsAvecPromotion>(entity =>
            {
                entity.ToView("vw_MaillotsAvecPromotion", "Maillots");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
