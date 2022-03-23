using Microsoft.EntityFrameworkCore;
using ProjectFirstSteps.Models;

namespace ProjectFirstSteps.Models
{
    public class MyContext:DbContext
    {
        public DbSet<Membre> Membres { get; set; }
        public DbSet<MembreMembre> MembreMembres { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Chat> Chats { get; set; }
        //public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PhotoVideo> PhotoVideos { get; set; }
        public DbSet<Lien> Liens { get; set; }
        public DbSet<Role> Roles { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<MakingPublications>();

            modelBuilder.Entity<Membre>(entity =>
            {
                entity.HasKey(m => m.Email);

                entity.Property(m => m.Nom)
                .IsRequired()
                .HasColumnType("varchar(30)");

                entity.Property(p => p.Prenom)
                .IsRequired()
                .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<MakingPublications>(entity =>
            {
                entity.HasNoKey();
            });

            //modelBuilder.Entity<Message>().ToTable("messages");
            //modelBuilder.Entity<Lien>().ToTable("liens");
            //modelBuilder.Entity<PhotoVideo>().ToTable("photosVideo");

            //Here Emmanuel Codes
            modelBuilder.Entity<MembreMembre>(entity =>
            {
                entity.HasKey(pc => new {pc.MailMembre2, pc.MailMembre1 });
                entity.Property(m => m.MailMembre1)
                .IsRequired()
                .HasColumnType("varchar(255)");

                entity.Property(m => m.MailMembre2)
                .IsRequired()
                .HasColumnType("varchar(255)");

            });

            modelBuilder.Entity<Invitation>(entity =>
            {
                entity.Property(m => m.InviterMail)
                .IsRequired()
                .HasColumnType("varchar(255)");

                entity.Property(m => m.InvitedMail)
                .IsRequired()
                .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.Property(m => m.MailMembre1)
                .IsRequired()
                .HasColumnType("varchar(255)");

                entity.Property(m => m.MailMembre2)
                .IsRequired()
                .HasColumnType("varchar(255)");
            });

        }

    }
}
