using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Bieren.DataLayer.Models
{
    public partial class BierenDbContext : DbContext
    {
        public BierenDbContext()
        {
        }

        public BierenDbContext(DbContextOptions<BierenDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DbBier> DbBiers { get; set; }
        public virtual DbSet<DbBrouwer> DbBrouwers { get; set; }
        public virtual DbSet<DbSoort> DbSoorts { get; set; }
        public virtual DbSet<DbUser> DbUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=BierenEnUsersDb;Integrated Security=True;Pooling=False");               
                    //"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\DATA\\SYNTRA\\Data\\BierenDb.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DbBier>(entity =>
            {
                entity.HasKey(e => e.BierNr)
                    .HasName("PK_Bieren");

                entity.ToTable("DbBier");

                entity.Property(e => e.Naam)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BrouwerNrNavigation)
                    .WithMany(p => p.DbBiers)
                    .HasForeignKey(d => d.BrouwerNr)
                    .HasConstraintName("FK_Bieren_Brouwers");

                entity.HasOne(d => d.SoortNrNavigation)
                    .WithMany(p => p.DbBiers)
                    .HasForeignKey(d => d.SoortNr)
                    .HasConstraintName("FK_Bieren_Soorten");
                entity.HasMany(e => e.Users);

            });

            modelBuilder.Entity<DbBrouwer>(entity =>
            {
                entity.HasKey(e => e.BrouwerNr)
                    .HasName("PK_Brouwers");

                entity.ToTable("DbBrouwer");

                entity.Property(e => e.Adres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrNaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gemeente)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DbSoort>(entity =>
            {
                entity.HasKey(e => e.SoortNr)
                    .HasName("PK_Soorten");

                entity.ToTable("DbSoort");

                entity.Property(e => e.Soort)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DbUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                .HasName("PK_Users");
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
                entity.ToTable("DbUser");
                entity.Property(e => e.Voornaam)
                    .HasMaxLength(50)
                    .IsUnicode(false); //indien false: varchar(50) indien true: nvarchar(50)
                entity.Property(e => e.Familienaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.GeboorteDatum).HasColumnType(nameof(System.DateTime));
                entity.HasMany(e => e.FavorieteBieren);
            });
        
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
