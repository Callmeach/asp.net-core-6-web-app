using Microsoft.EntityFrameworkCore;

namespace ProjectFirstSteps.Models
{
    public class MyContext:DbContext
    {
        public DbSet<Membre> Membres { get; set; }
        public DbSet<Administrateur> Administrateurs { get; set; }
        public DbSet<Moderateur> Moderateurs { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PhotoVideo> PhotoVideos { get; set; }
        public DbSet<Lien> Liens { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity < Membre>(entity =>
            {
                entity.HasKey(m => m.Email);

                entity.Property(m => m.Nom)
                .IsRequired()
                .HasColumnType("varchar(30)");

                entity.Property(p => p.Prenom)
                .IsRequired()
                .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Administrateur>(entity =>
            {
                entity.HasKey(a => a.Email);

                entity.Property(m => m.Nom)
                .IsRequired()
                .HasColumnType("varchar(30)");

                entity.Property(p => p.Prenom)
                .IsRequired()
                .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Moderateur>(entity =>
            {
                entity.HasKey(a => a.Email);

                entity.Property(m => m.Nom)
                .IsRequired()
                .HasColumnType("varchar(30)");

                entity.Property(p => p.Prenom)
                .IsRequired()
                .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.HasKey(cle => new { cle.dateDePublication, cle.uneRessource, cle.memberMail });
                entity.HasOne(v => v.Membre).WithMany(p => p.Publications).HasForeignKey(c => c.memberMail);
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.HasOne<Ressource>(i => i.Ressource).WithMany(m => m.lesPublications).HasForeignKey(c => c.uneRessource);
            });

            modelBuilder.Entity<Ressource>(entity =>
            {
                entity.HasKey(k => k.nomRessource);
            });
        }

    }
}
