using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MusicShop.WebAPI.Model
{
    public partial class MusicShopContext : DbContext
    {
        public MusicShopContext()
        {
        }

        public MusicShopContext(DbContextOptions<MusicShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Includes> Includes { get; set; }
        public virtual DbSet<PaymentCard> PaymentCards { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.AlId);

                entity.ToTable("ALBUM");

                entity.Property(e => e.AlId).HasColumnName("AL_ID");

                entity.Property(e => e.AlDateRelease)
                    .HasColumnName("AL_DATE_RELEASE")
                    .HasColumnType("datetime");

                entity.Property(e => e.AlDesc)
                    .HasColumnName("AL_DESC")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.AlImage)
                    .HasColumnName("AL_IMAGE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.AlName)
                    .IsRequired()
                    .HasColumnName("AL_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.AuId);

                entity.ToTable("AUTHOR");

                entity.HasIndex(e => e.PubId)
                    .HasName("PUBLISH_FK");

                entity.Property(e => e.AuId).HasColumnName("AU_ID");

                entity.Property(e => e.AuDesc)
                    .HasColumnName("AU_DESC")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.AuImage)
                    .HasColumnName("AU_IMAGE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.AuName)
                    .IsRequired()
                    .HasColumnName("AU_NAME")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.AuWebsite)
                    .HasColumnName("AU_WEBSITE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.PubId).HasColumnName("PUB_ID");

                entity.HasOne(d => d.Pub)
                    .WithMany(p => p.Author)
                    .HasForeignKey(d => d.PubId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_AUTHOR_PUBLISH_BY_PUBLISHER");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClId);

                entity.ToTable("CLIENT");

                entity.Property(e => e.ClId).HasColumnName("CL_ID");

                entity.Property(e => e.ClEmail)
                    .HasColumnName("CL_EMAIL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClLogin)
                    .IsRequired()
                    .HasColumnName("CL_LOGIN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClName)
                    .IsRequired()
                    .HasColumnName("CL_NAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ClPassword)
                    .IsRequired()
                    .HasColumnName("CL_PASSWORD")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClPhone)
                    .IsRequired()
                    .HasColumnName("CL_PHONE")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("GENRE");

                entity.Property(e => e.GenreId).HasColumnName("GENRE_ID");

                entity.Property(e => e.GenreDesc)
                    .HasColumnName("GENRE_DESC")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.GenreImage)
                    .HasColumnName("GENRE_IMAGE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.GenreName)
                    .IsRequired()
                    .HasColumnName("GENRE_NAME")
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Includes>(entity =>
            {
                entity.HasKey(e => new { e.SongId, e.ClId, e.PurId })
                    .HasName("PK_INCLUDES");

                entity.HasIndex(e => e.SongId)
                    .HasName("Includes_FK");

                entity.HasIndex(e => new { e.ClId, e.PurId })
                    .HasName("Includes2_FK");

                entity.Property(e => e.SongId).HasColumnName("SONG_ID");

                entity.Property(e => e.ClId).HasColumnName("CL_ID");

                entity.Property(e => e.PurId).HasColumnName("PUR_ID");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Includes)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INCLUDES_INCLUDES_SONG");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.Includes)
                    .HasForeignKey(d => new { d.ClId, d.PurId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INCLUDES_INCLUDES2_PURCHASE");
            });

            modelBuilder.Entity<PaymentCard>(entity =>
            {
                entity.HasKey(e => new { e.ClId, e.PayId });

                entity.ToTable("PAYMENT_CARD");

                entity.HasIndex(e => e.ClId)
                    .HasName("BIND_TO_FK");

                entity.Property(e => e.ClId).HasColumnName("CL_ID");

                entity.Property(e => e.PayId)
                    .HasColumnName("PAY_ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.PayCvc)
                    .IsRequired()
                    .HasColumnName("PAY_CVC")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.PayExpireDate)
                    .HasColumnName("PAY_EXPIRE_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.PayHolder)
                    .IsRequired()
                    .HasColumnName("PAY_HOLDER")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PayNumber)
                    .IsRequired()
                    .HasColumnName("PAY_NUMBER")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cl)
                    .WithMany(p => p.PaymentCard)
                    .HasForeignKey(d => d.ClId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PAYMENT__BIND_TO_CLIENT");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.PubId);

                entity.ToTable("PUBLISHER");

                entity.Property(e => e.PubId).HasColumnName("PUB_ID");

                entity.Property(e => e.PubImage)
                    .HasColumnName("PUB_IMAGE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.PubName)
                    .IsRequired()
                    .HasColumnName("PUB_NAME")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PubWebsite)
                    .HasColumnName("PUB_WEBSITE")
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => new { e.ClId, e.PurId });

                entity.ToTable("PURCHASE");

                entity.HasIndex(e => e.ClId)
                    .HasName("PAYS_FK");

                entity.Property(e => e.ClId).HasColumnName("CL_ID");

                entity.Property(e => e.PurId)
                    .HasColumnName("PUR_ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.PurDate)
                    .HasColumnName("PUR_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.TotalQty).HasColumnName("TOTAL_QTY");

                entity.Property(e => e.TotalSum).HasColumnName("TOTAL_SUM");

                entity.HasOne(d => d.Cl)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.ClId)
                    .HasConstraintName("FK_PURCHASE_PAYS_CLIENT");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("SONG");

                entity.HasIndex(e => e.AlId)
                    .HasName("CONSIST_OF_FK");

                entity.HasIndex(e => e.AuId)
                    .HasName("OWNS_FK");

                entity.HasIndex(e => e.GenreId)
                    .HasName("BELONGS_TO_FK");

                entity.Property(e => e.SongId).HasColumnName("SONG_ID");

                entity.Property(e => e.AlId).HasColumnName("AL_ID");

                entity.Property(e => e.AuId).HasColumnName("AU_ID");

                entity.Property(e => e.Duration).HasColumnName("DURATION");

                entity.Property(e => e.GenreId).HasColumnName("GENRE_ID");

                entity.Property(e => e.License)
                    .HasColumnName("LICENSE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Lyrics)
                    .HasColumnName("LYRICS")
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.SongName)
                    .IsRequired()
                    .HasColumnName("SONG_NAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Al)
                    .WithMany(p => p.Song)
                    .HasForeignKey(d => d.AlId)
                    .HasConstraintName("FK_ALBUM_CONSIST_OF_SONGS");

                entity.HasOne(d => d.Au)
                    .WithMany(p => p.Song)
                    .HasForeignKey(d => d.AuId)
                    .HasConstraintName("FK_SONG_OWNS_AUTHOR");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Song)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_SONG_BELONGS_T_GENRE");
            });
        }
    }
}
