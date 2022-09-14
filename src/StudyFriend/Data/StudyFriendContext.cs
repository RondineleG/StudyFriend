using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyFriend.Models;

namespace StudyFriend.Data
{
    public class StudyFriendContext : IdentityDbContext<ApplicationUser>
    {
        public StudyFriendContext(DbContextOptions<StudyFriendContext> options)
            : base(options)
        {
        }

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
        }

    }
}
