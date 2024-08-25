using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ChatStuff.Core.Entities;

namespace ChatStuff.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ChatStuffUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Blocks> Blocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FriendRequest>(entity =>
            {
                entity.HasKey(fr => fr.Id);

                // Configure SourceUserName as a foreign key
                entity.HasOne<ChatStuffUser>()
                    .WithMany()
                    .HasForeignKey(fr => fr.SourceUserName)
                    .HasPrincipalKey(u => u.UserName)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure TargetUserName as a foreign key
                entity.HasOne<ChatStuffUser>()
                    .WithMany()
                    .HasForeignKey(fr => fr.TargetUserName)
                    .HasPrincipalKey(u => u.UserName)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Friends>(entity =>
            {
                entity.HasKey(f => f.Id);

                // Configure FriendName1 as a foreign key
                entity.HasOne<ChatStuffUser>()
                    .WithMany()
                    .HasForeignKey(f => f.FriendName1)
                    .HasPrincipalKey(u => u.UserName)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure FriendName2 as a foreign key
                entity.HasOne<ChatStuffUser>()
                    .WithMany()
                    .HasForeignKey(f => f.FriendName2)
                    .HasPrincipalKey(u => u.UserName)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Blocks>(entity =>
            {
                entity.HasKey(f => f.Id);

                // Configure SourceUserName as a foreign key
                entity.HasOne<ChatStuffUser>()
                    .WithMany()
                    .HasForeignKey(f => f.SourceUserName)
                    .HasPrincipalKey(u => u.UserName)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure TargetUserName as a foreign key
                entity.HasOne<ChatStuffUser>()
                    .WithMany()
                    .HasForeignKey(f => f.TargetUserName)
                    .HasPrincipalKey(u => u.UserName)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}