using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Grazer.Models;

namespace Grazer.Data
{
    public class GrazerDbContext : IdentityDbContext<User, Role, string>
    {
        private readonly IConfiguration _config;

        public GrazerDbContext(DbContextOptions<GrazerDbContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();

            base.OnModelCreating(builder);

            builder.Entity<User>(entity => { entity.ToTable("users"); });
            builder.Entity<Role>(entity => { entity.ToTable("roles"); });
            builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("user_claims"); });
            builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("user_logins"); });
            builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("user_tokens"); });
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("user_roles"); });
            builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("role_claims"); });

            var admin = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                EmailConfirmed = true
            };
            admin.PasswordHash = hasher.HashPassword(admin, _config.GetValue<string>("AdminPassword", "admin"));

            var adminRole = new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var memberRole = new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Member",
                NormalizedName = "MEMBER"
            };

            builder.Entity<User>().HasData(admin);
            builder.Entity<Role>().HasData(adminRole, memberRole);
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = admin.Id,
                    RoleId = adminRole.Id
                });
        }

        public DbSet<Post> Posts { get; set; }
    }
}
