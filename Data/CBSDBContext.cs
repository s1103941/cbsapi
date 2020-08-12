using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace cbstest.Models
{
    public partial class CBSDBContext : DbContext
    {
        public CBSDBContext()
        {
        }

        public CBSDBContext(DbContextOptions<CBSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bedrijf> Bedrijf { get; set; }
        public virtual DbSet<Opgave> Opgave { get; set; }
        public virtual DbSet<Periode> Periode { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLExpress;Database=CBSDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bedrijf>(entity =>
            {
                entity.HasKey(e => e.BeId);

                entity.Property(e => e.BeId)
                    .HasColumnName("be_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Naam)
                    .HasColumnName("naam")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Opgave>(entity =>
            {
                entity.Property(e => e.OpgaveId)
                    .HasColumnName("opgave_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.BeId)
                    .HasColumnName("be_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Btw)
                    .HasColumnName("btw")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.OmzetInclusiefbtw)
                    .HasColumnName("omzet_inclusiefbtw")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.PeriodeId)
                    .HasColumnName("periode_id")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Be)
                    .WithMany(p => p.Opgave)
                    .HasForeignKey(d => d.BeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Opgave_Bedrijf");

                entity.HasOne(d => d.Periode)
                    .WithMany(p => p.Opgave)
                    .HasForeignKey(d => d.PeriodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Opgave_Periode");
            });

            modelBuilder.Entity<Periode>(entity =>
            {
                entity.Property(e => e.PeriodeId)
                    .HasColumnName("periode_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Begindatum)
                    .HasColumnName("begindatum")
                    .HasColumnType("datetime");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Einddatum)
                    .HasColumnName("einddatum")
                    .HasColumnType("datetime");

                entity.Property(e => e.Periodetype)
                    .IsRequired()
                    .HasColumnName("periodetype")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
