using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DomaZdravlja.Models
{
    public partial class DomZdravljaContext : DbContext
    {
        public DomZdravljaContext()
        {
        }

        public DomZdravljaContext(DbContextOptions<DomZdravljaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Izvestaji> Izvestaji { get; set; }
        public virtual DbSet<KartonPacijenta> KartonPacijenta { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<PreglediPacijenta> PreglediPacijenta { get; set; }
        public virtual DbSet<Recept> Recept { get; set; }
        public virtual DbSet<ReceptKartonVeza> ReceptKartonVeza { get; set; }
        public virtual DbSet<Rola> Rola { get; set; }
        public virtual DbSet<VrstaIzvestaja> VrstaIzvestaja { get; set; }
        public virtual DbSet<PacijentKorisnika> PacijentKorisnika { get; set; }
        public virtual DbSet<PreglediPacijentaKarton> PreglediPacijentaKarton { get; set; }

        //public virtual DbSet<KorisnikRola> KorisnikRola { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-UR8376F;Database=DomZdravlja;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Izvestaji>(entity =>
            {
                entity.HasKey(e => e.IdIzvestaj);

                entity.Property(e => e.IdIzvestaj).HasColumnName("idIzvestaj");

                entity.Property(e => e.AktivanIzvestaj).HasColumnName("aktivanIzvestaj");

                entity.Property(e => e.DatumIzvestaj)
                    .HasColumnName("datumIzvestaj")
                    .HasColumnType("date");

                entity.Property(e => e.IdVrsteIzvestaja).HasColumnName("idVrsteIzvestaja");

                entity.Property(e => e.LekarSpecijalista)
                    .IsRequired()
                    .HasColumnName("lekarSpecijalista")
                    .HasMaxLength(50);

                entity.Property(e => e.NazivUstanove)
                    .IsRequired()
                    .HasColumnName("nazivUstanove")
                    .HasMaxLength(50);

                entity.Property(e => e.OpisIzvestaj)
                    .IsRequired()
                    .HasColumnName("opisIzvestaj");
                entity.Property(e => e.ImePacijenta).IsRequired().HasColumnName("imePacijenta").HasMaxLength(20);
                entity.Property(e => e.PrezimePacijenta).IsRequired().HasColumnName("prezimePacijenta").HasMaxLength(20);
                entity.HasOne(d => d.IdVrsteIzvestajaNavigation)
                    .WithMany(p => p.Izvestaji)
                    .HasForeignKey(d => d.IdVrsteIzvestaja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Izvestaji_VrstaIzvestaja");
            });

            modelBuilder.Entity<KartonPacijenta>(entity =>
            {
                entity.HasKey(e => e.IdKarton);

                entity.Property(e => e.IdKarton).HasColumnName("idKarton");

                entity.Property(e => e.AktivanKarton).HasColumnName("aktivanKarton");

                entity.Property(e => e.BrojKnjizice).HasColumnName("brojKnjizice");

                entity.Property(e => e.DatumOtvaranjaKartona)
                    .HasColumnName("datumOtvaranjaKartona")
                    .HasColumnType("date");

                entity.Property(e => e.IdKorisnik).HasColumnName("idKorisnik");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasColumnName("ime")
                    .HasMaxLength(50);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasColumnName("prezime")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdKorisnikNavigation)
                    .WithMany(p => p.KartonPacijenta)
                    .HasForeignKey(d => d.IdKorisnik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KartonPacijenta_Korisnik");
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.HasKey(e => e.IdKorisnik);

                entity.Property(e => e.IdKorisnik).HasColumnName("idKorisnik");

                entity.Property(e => e.AktivanKorisnik).HasColumnName("aktivanKorisnik");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(20);

                entity.Property(e => e.IdRola).HasColumnName("idRola");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasColumnName("ime")
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasColumnName("prezime")
                    .HasMaxLength(20);

                entity.Property(e => e.Specijalnost)
                    .IsRequired()
                    .HasColumnName("specijalnost")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdRolaNavigation)
                    .WithMany(p => p.Korisnik)
                    .HasForeignKey(d => d.IdRola)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Korisnik_Rola");
            });

            modelBuilder.Entity<PreglediPacijenta>(entity =>
            {
                entity.HasKey(e => e.IdPregleda);

                entity.Property(e => e.IdPregleda).HasColumnName("idPregleda");

                entity.Property(e => e.AktivanPregled).HasColumnName("aktivanPregled");

                entity.Property(e => e.DatumIvremePregleda)
                    .HasColumnName("datumIVremePregleda")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdKarton).HasColumnName("idKarton");

                entity.HasOne(d => d.IdKartonNavigation)
                    .WithMany(p => p.PreglediPacijenta)
                    .HasForeignKey(d => d.IdKarton)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreglediPacijenta_KartonPacijenta");
            });

            modelBuilder.Entity<Recept>(entity =>
            {
                entity.HasKey(e => e.IdRecepta);

                entity.Property(e => e.IdRecepta).HasColumnName("idRecepta");

                entity.Property(e => e.AktivanRecept).HasColumnName("aktivanRecept");

                entity.Property(e => e.Kolicina).HasColumnName("kolicina");

                entity.Property(e => e.NazivLeka)
                    .IsRequired()
                    .HasColumnName("nazivLeka")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ReceptKartonVeza>(entity =>
            {
                entity.HasKey(e => e.IdVeze);

                entity.Property(e => e.IdVeze).HasColumnName("idVeze");

                entity.Property(e => e.DatumIzadavanja)
                    .HasColumnName("datumIzadavanja")
                    .HasColumnType("date");

                entity.Property(e => e.IdKarton).HasColumnName("idKarton");

                entity.Property(e => e.IdRecepta).HasColumnName("idRecepta");

                entity.HasOne(d => d.IdKartonNavigation)
                    .WithMany(p => p.ReceptKartonVeza)
                    .HasForeignKey(d => d.IdKarton)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceptKartonVeza_KartonPacijenta");

                entity.HasOne(d => d.IdReceptaNavigation)
                    .WithMany(p => p.ReceptKartonVeza)
                    .HasForeignKey(d => d.IdRecepta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReceptKartonVeza_Recept");
            });

            modelBuilder.Entity<Rola>(entity =>
            {
                entity.HasKey(e => e.IdRola);

                entity.Property(e => e.IdRola).HasColumnName("idRola");

                entity.Property(e => e.NazivRola)
                    .IsRequired()
                    .HasColumnName("nazivRola")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VrstaIzvestaja>(entity =>
            {
                entity.HasKey(e => e.IdVrsteIzvestaja);

                entity.Property(e => e.IdVrsteIzvestaja).HasColumnName("idVrsteIzvestaja");

                entity.Property(e => e.IdKorisnik).HasColumnName("idKorisnik");

                entity.Property(e => e.NazivVrsteIzvestaja)
                    .IsRequired()
                    .HasColumnName("nazivVrsteIzvestaja")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdKorisnikNavigation)
                    .WithMany(p => p.VrstaIzvestaja)
                    .HasForeignKey(d => d.IdKorisnik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VrstaIzvestaja_Korisnik");
            });
        }
    }
}
