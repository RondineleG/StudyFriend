using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyJourney.Models;

namespace StudyJourney.Data
{
    public class StudyJourneyDbContext : IdentityDbContext<ApplicationUser>
    {
        public StudyJourneyDbContext(DbContextOptions<StudyJourneyDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Topic> Topic { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(
                typeBuilder =>
                {
                    typeBuilder.HasMany(user => user.Topics)
                        .WithOne(topic => topic.ApplicationUser)
                        .HasForeignKey(topic => topic.UserId)
                        .IsRequired();
                });

            builder.Entity<Topic>(
                typeBuilder =>
                {
                    typeBuilder.HasOne(topic => topic.ApplicationUser)
                        .WithMany(user => user.Topics)
                        .HasForeignKey(topic => topic.UserId)
                        .IsRequired();
                });

            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity => { entity.ToTable(name: "User"); });
            builder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Role"); });
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });
            builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
        }

    }
}
