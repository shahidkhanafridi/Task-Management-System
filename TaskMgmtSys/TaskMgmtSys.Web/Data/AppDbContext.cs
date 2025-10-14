using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TaskMgmtSys.Web.Entities;

namespace TaskMgmtSys.Web.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, long,
            AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ---------------- IDENTITY ----------------
            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<AppRole>().ToTable("Roles");
            builder.Entity<AppUserRole>().ToTable("UserRoles");
            builder.Entity<AppUserClaim>().ToTable("UserClaims");
            builder.Entity<AppUserLogin>().ToTable("UserLogins");
            builder.Entity<AppRoleClaim>().ToTable("RoleClaims");
            builder.Entity<AppUserToken>().ToTable("UserTokens");

            builder.Entity<AppUser>(entity =>
            {
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.LastName).HasMaxLength(100);
                entity.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(u => u.IsDeleted).IsRequired().HasDefaultValue(false);
                entity.HasOne(u => u.CreatedByUser).WithMany().HasForeignKey(u => u.CreatedBy).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(u => u.UpdatedByUser).WithMany().HasForeignKey(u => u.UpdatedBy).OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.UserProjects).WithOne(up => up.User).HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AppRole>(entity =>
            {
                entity.Property(r => r.Description).HasMaxLength(500);
            });

            builder.Entity<AppUserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AppUserClaim>(entity =>
            {
                entity.HasOne(uc => uc.User).WithMany(u => u.Claims).HasForeignKey(uc => uc.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AppRoleClaim>(entity =>
            {
                entity.HasOne(rc => rc.Role).WithMany(r => r.RoleClaims).HasForeignKey(rc => rc.RoleId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AppUserLogin>(entity =>
            {
                entity.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
                entity.HasOne(ul => ul.User).WithMany(u => u.Logins).HasForeignKey(ul => ul.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AppUserToken>(entity =>
            {
                entity.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
                entity.HasOne(ut => ut.User).WithMany(u => u.Tokens).HasForeignKey(ut => ut.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.ProjectName).IsRequired().HasMaxLength(300);
                entity.Property(p => p.Description).HasMaxLength(5000);
                entity.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

                entity.HasMany(p => p.UserProjects).WithOne(up => up.Project).HasForeignKey(up => up.ProjectId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<UserProject>(entity =>
            {
                entity.HasKey(up => up.UserProjectId);
                entity.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);

                entity.HasOne(up => up.User).WithMany(u => u.UserProjects).HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(up => up.Project).WithMany(p => p.UserProjects).HasForeignKey(up => up.ProjectId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
