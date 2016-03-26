using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationJedi.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant plus de propriétés à votre classe ApplicationUser ; consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    
    public class ApplicationUser : IdentityUser
    {
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public int Points { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*modelBuilder.Entity<MyUser>().ToTable("UsersDetails").Property(p => p.Email);
            modelBuilder.Entity<MyUser>().ToTable("UsersDetails").Property(p => p.EmailConfirmed);
            modelBuilder.Entity<MyUser>().ToTable("UsersDetails").Property(p => p.SecurityStamp);
            modelBuilder.Entity<MyUser>().ToTable("UsersDetails").Property(p => p.PhoneNumber);
            modelBuilder.Entity<MyUser>().ToTable("UsersDetails").Property(p => p.PhoneNumberConfirmed);
            modelBuilder.Entity<MyUser>().ToTable("UsersDetails").Property(p => p.TwoFactorEnabled);
            modelBuilder.Entity<MyUser>().ToTable("UsersDetails").Property(p => p.LockoutEnabled);
            modelBuilder.Entity<MyUser>().ToTable("UsersDetails").Property(p => p.LockoutEndDateUtc);
            modelBuilder.Entity<MyUser>().ToTable("UsersDetails").Property(p => p.AccessFailedCount);*/

            //modelBuilder.Entity<MyUser>().ToTable("Users").HasKey(m => m.Id);
            //modelBuilder.Entity<MyUser>().ToTable("Users").Property(p => p.Id).HasColumnName("Ident");
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("Id2");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("Id2");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.UserName).HasColumnName("Login");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.PasswordHash).HasColumnName("Password");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Points).HasColumnName("Points");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Prenom).HasColumnName("Prenom");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Nom).HasColumnName("Nom");
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}